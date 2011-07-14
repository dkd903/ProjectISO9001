using System;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Criterion;
using SuperSchnell.Project.Domain;
using SuperSchnell.Project.Mvc.EntityUpdaters;
using SuperSchnell.Project.Mvc.Models;
using SuperSchnell.Project.Mvc.Models.Customer;
using SuperSchnell.Project.Mvc.Repositories;

namespace SuperSchnell.Project.Mvc.Controllers
{
    public class CustomerController : Controller
    {
        private IRepository _repository;

        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var options = new ResultOptions();
            var customers = _repository.WrapQueryInTransaction(s => FindCustomersQuery(s,string.Empty,options));
            return View(new PagedViewModel<ICustomer>(customers,options));
        }
        public ActionResult Edit(long id)
        {
            var customer = _repository.WrapQueryInTransaction(s => s.Get<Customer>(id));
            return DoEdit(customer);
        }
        public ActionResult Create()
        {
            return DoEdit(new Customer());
        }
        [HttpPost]
        public ActionResult Save(EditCustomerViewModel viewModel)
        {
            _repository.TrySaveUpdate(new CustomerUpdater(viewModel));
            return RedirectToAction("Index");
        }
        private ActionResult DoEdit(Customer customer)
        {
            var viewModel = new EditCustomerViewModel(customer);
            return View("Edit",viewModel);
        }
        public ActionResult Delete(long id, int version)
        {
            _repository.TryDelete<Customer>(id, version);
            return RedirectToAction("Index");
        }
        private PagedResultSet<ICustomer> FindCustomersQuery(ISession session, string searchString, ResultOptions options)
        {
            var countQuery = CustomersQuery(session, searchString)
                .Select(Projections.RowCount())
                .FutureValue<int>();
            var result = CustomersQuery(session, searchString)
                .LimitResultSet(options)
                .Future<ICustomer>();
            return new PagedResultSet<ICustomer>(result.ToList(),countQuery.Value);
        }

        private IQueryOver<Customer,Customer> CustomersQuery(ISession session, string searchString)
        {
            var criteria =
                Restrictions.Or(Restrictions.On<Customer>(c => c.AccountNumber).IsLike(searchString, MatchMode.Start),
                                Restrictions.On<Customer>(c => c.CompanyName).IsLike(searchString, MatchMode.Start));
            return session.QueryOver<Customer>()
                .Where(criteria);
        }
    }
}