using System.Linq;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Criterion;
using SuperSchnell.Project.Domain;
using SuperSchnell.Project.Mvc.EntityUpdaters;
using SuperSchnell.Project.Mvc.Models;
using SuperSchnell.Project.Mvc.Models.CoolingProject;
using SuperSchnell.Project.Mvc.Repositories;

namespace SuperSchnell.Project.Mvc.Controllers
{
    [Authorize]
    public class CoolingProjectController : Controller
    {
        private readonly IRepository _repository;

        public CoolingProjectController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int page = 0, int pageSize = 20)
        {
            var options = new ResultOptions {Page = page, PageSize = pageSize};
            var projects = _repository.WrapQueryInTransaction(s => FindProjectsQuery(s, options));
            return View(new PagedViewModel<ICoolingProject>(projects, options));
        }

        public ActionResult Create()
        {
            return DoEdit(new CoolingProject());
        }

        public ActionResult Edit(long id)
        {
            var project = _repository.WrapQueryInTransaction(s => s.Get<CoolingProject>(id));
            return DoEdit(project);
        }

        public ActionResult Details(long id)
        {
            var project = _repository.WrapQueryInTransaction(s => s.Get<CoolingProject>(id));
            return View(project);
        }

        private ActionResult DoEdit(ICoolingProject project)
        {
            return View("Edit", new EditCoolingProjectViewModel(project));
        }

        [HttpPost]
        public ActionResult Save(EditCoolingProjectViewModel viewModel)
        {
            var result = _repository.TrySaveUpdate(new CoolingProjectUpdater(viewModel));
            return result.Success
                       ? RedirectToAction("Details", new {id = result.AssignedId})
                       : RedirectToAction("Index");
        }

        public ActionResult Delete(long id, int version)
        {
            _repository.TryDelete<CoolingProject>(id, version);
            return RedirectToAction("Index");
        }

        private PagedResultSet<ICoolingProject> FindProjectsQuery(ISession session, ResultOptions options)
        {
            var countQuery = ProjectsQuery(session)
                .Select(Projections.RowCount())
                .FutureValue<int>();
            var result = ProjectsQuery(session)
                .LimitResultSet(options)
                .Future<ICoolingProject>();
            return new PagedResultSet<ICoolingProject>(result.ToList(), countQuery.Value);
        }

        private IQueryOver<CoolingProject, CoolingProject> ProjectsQuery(ISession session)
        {
            return session.QueryOver<CoolingProject>();
        }
    }
}