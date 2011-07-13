namespace SuperSchnell.Project.Domain
{
    public class Customer : Entity<ICustomer>, ICustomer
    {
        public virtual string AccountNumber { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Street { get; set; }
        public virtual string PlaceName { get; set; }
        public virtual string Zip { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
    }
}