using System.Collections.Generic;

namespace SuperSchnell.Project.Domain
{
    public interface IEntity:IHasId
    {
        int Version { get; }
        void Delete();
        bool IsValid(out IEnumerable<string> errors);
    }
}