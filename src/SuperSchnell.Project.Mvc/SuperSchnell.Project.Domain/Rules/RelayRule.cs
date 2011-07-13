using System;

namespace SuperSchnell.Project.Domain.Rules
{
    public class RelayRule<T> : IRule<T>
    {
        private readonly Predicate<T> _isBroken;

        public RelayRule(Predicate<T> isBroken, string brokenMessage)
        {
            _isBroken = isBroken;
            BrokenMessage = brokenMessage;
        }
        public string BrokenMessage { get; private set; }
        public bool IsBroken(T entity)
        {
            return _isBroken(entity);
        }
    }
}