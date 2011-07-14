using System.Collections.Generic;
using NHibernate;
using SuperSchnell.Project.Domain;
using SuperSchnell.Project.Mvc.Models.Customer;

namespace SuperSchnell.Project.Mvc.EntityUpdaters
{
    public class CustomerUpdater : EntityUpdater<Customer>
    {
        private readonly EditCustomerViewModel _viewModel;

        public CustomerUpdater(EditCustomerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override long Id
        {
            get { return _viewModel.Id; }
        }

        public override int Version
        {
            get { return _viewModel.Version; }
        }

        public override bool Update(ISession session, out IEnumerable<string> errors)
        {
            _entity.AccountNumber = _viewModel.AccountNumber;
            _entity.City = _viewModel.City ?? string.Empty;
            _entity.Zip = _viewModel.Zip ?? string.Empty;
            _entity.Country = _viewModel.Country ?? string.Empty;
            _entity.CompanyName = _viewModel.CompanyName ?? string.Empty;
            _entity.PlaceName = _viewModel.PlaceName ?? string.Empty;
            _entity.Street = _viewModel.Street ?? string.Empty;
            errors = new string[0];
            return true;
        }

        public override Customer CreateNewEntity(ISession session)
        {
            return new Customer();
        }
    }
}