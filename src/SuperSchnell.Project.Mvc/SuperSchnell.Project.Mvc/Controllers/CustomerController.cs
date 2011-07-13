using System.Web.Mvc;
using SuperSchnell.Project.Mvc.Repositories;

namespace SuperSchnell.Project.Mvc.Controllers
{
    public class CustomerController : Controller
    {
        private NHibernateRepository _repo;

        public CustomerController(NHibernateRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}