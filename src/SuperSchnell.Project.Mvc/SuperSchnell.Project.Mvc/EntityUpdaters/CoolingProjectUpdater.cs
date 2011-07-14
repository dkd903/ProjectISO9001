using System;
using System.Collections.Generic;
using NHibernate;
using SuperSchnell.Project.Domain;
using SuperSchnell.Project.Mvc.Models.CoolingProject;

namespace SuperSchnell.Project.Mvc.EntityUpdaters
{
    public class CoolingProjectUpdater : EntityUpdater<CoolingProject>
    {
        private readonly EditCoolingProjectViewModel _viewModel;

        public CoolingProjectUpdater(EditCoolingProjectViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool Update(ISession session, out IEnumerable<string> errors)
        {
            _entity.Description = _viewModel.Description;
            if (CustomerHasChanged())
            {
                _entity.Customer = !_viewModel.CustomerId.HasValue
                                       ? null
                                       : session.Get<Customer>(_viewModel.CustomerId.Value);
            }
            errors = new string[0];
            return true;
        }

        private bool CustomerHasChanged()
        {
            if (_entity.Customer == null && !_viewModel.CustomerId.HasValue)
                return false; //No change - both are null
            if (_entity.Customer == null || !_viewModel.CustomerId.HasValue)
                return true; //Either it started out null or it will change into null
            return _viewModel.CustomerId.Value != _entity.Customer.Id;
        }

        public override long Id
        {
            get { return _viewModel.Id; }
        }

        public override int Version
        {
            get { return _viewModel.Version; }
        }

        public override CoolingProject CreateNewEntity(ISession session)
        {
            return new CoolingProject();
        }
    }
}