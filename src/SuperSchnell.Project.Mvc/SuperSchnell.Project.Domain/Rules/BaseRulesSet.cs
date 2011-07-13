using System.Collections.Generic;
using System.Linq;

namespace SuperSchnell.Project.Domain.Rules
{
    public class BaseRulesSet<T> : IRulesSet<T>
    {
        private readonly IList<IRule<T>> _rules =
            new List<IRule<T>>();

        public void AddRule(IRule<T> rule)
        {
            _rules.Add(rule);
        }

        public bool UpholdsRules(T entity, out IEnumerable<string> errors)
        {
            errors = _rules.Where(r => r.IsBroken(entity)).Select(r => r.BrokenMessage);
            return !errors.Any();
        }
    }
}