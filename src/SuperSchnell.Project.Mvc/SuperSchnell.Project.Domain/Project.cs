using System.Collections.Generic;

namespace SuperSchnell.Project.Domain
{
    public class Project : Entity<IProject>, IProject
    {
        public virtual string Description { get; set; }
        public virtual ICustomer Customer { get; set; }
        public virtual IEnumerable<IProjectCalculation> Calculations { get; private set; }
    }
}