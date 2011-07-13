using System.Collections.Generic;

namespace SuperSchnell.Project.Domain
{
    public interface IProject:IEntity
    {
        string Description { get; set; }
        ICustomer Customer { get; set; }
        IEnumerable<IProjectCalculation> Calculations { get; }
    }
}