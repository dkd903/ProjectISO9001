using SuperSchnell.Project.Domain.Rules;

namespace SuperSchnell.Project.Domain
{
    public class Customer : Entity<ICustomer>, ICustomer
    {
        static Customer()
        {
            _validationRuleSet.AddRule(new RelayRule<ICustomer>(c => string.IsNullOrEmpty(c.AccountNumber), "SuperSchnell_Project_Domain_Validation_Customer_MissingAccountNumber"));
            _validationRuleSet.AddRule(new RelayRule<ICustomer>(c => string.IsNullOrEmpty(c.CompanyName), "SuperSchnell_Project_Domain_Validation_Customer_MissingCompanyName"));
        }
        public virtual string AccountNumber { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Street { get; set; }
        public virtual string PlaceName { get; set; }
        public virtual string Zip { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
    }
}