namespace SuperSchnell.Project.Domain
{
    public interface ICustomer:IEntity
    {
        string AccountNumber { get; set; }
        string CompanyName { get; set; }
        string Street { get; set; }
        string PlaceName { get; set; }
        string Zip { get; set; }
        string City { get; set; }
        string Country { get; set; }
    }
}