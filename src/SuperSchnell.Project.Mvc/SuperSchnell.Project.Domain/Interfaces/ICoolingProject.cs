using System.Collections.Generic;

namespace SuperSchnell.Project.Domain
{
    public interface ICoolingProject:IEntity
    {
        string Description { get; set; }
        ICustomer Customer { get; set; }
        IEnumerable<IProjectCalculation> Calculations { get; }
        string CustomerName { get; }
        string CustomerAccount { get; }
    }
}