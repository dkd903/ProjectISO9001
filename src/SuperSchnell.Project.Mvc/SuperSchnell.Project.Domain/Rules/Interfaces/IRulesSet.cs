using System.Collections.Generic;

namespace SuperSchnell.Project.Domain.Rules
{
    public interface IRulesSet<T>
    {
        void AddRule(IRule<T> rule);
        bool UpholdsRules(T entity, out IEnumerable<string> errors);
    }
}