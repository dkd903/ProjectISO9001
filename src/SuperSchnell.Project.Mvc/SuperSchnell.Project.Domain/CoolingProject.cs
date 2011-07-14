using System.Collections.Generic;
using SuperSchnell.Project.Domain.Rules;

namespace SuperSchnell.Project.Domain
{
    public class CoolingProject : Entity<ICoolingProject>, ICoolingProject
    {
        static CoolingProject()
        {
            _validationRuleSet.AddRule(new RelayRule<ICoolingProject>(cp => string.IsNullOrEmpty(cp.Description), "SuperSchnell_Project_Domain_Validation_CoolingProject_MissingDescription"));
        }
        public CoolingProject()
        {
            CustomerName = string.Empty;
            CustomerAccount = string.Empty;
        }
        public virtual string Description { get; set; }
        private ICustomer _customer;
        public virtual ICustomer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                CustomerName = _customer == null ? string.Empty : _customer.CompanyName;
                CustomerAccount = _customer == null ? string.Empty : _customer.AccountNumber;
            }
        }

        public virtual IEnumerable<IProjectCalculation> Calculations { get; private set; }
        public virtual string CustomerName { get; private set; }
        public virtual string CustomerAccount { get; private set; }
    }
}