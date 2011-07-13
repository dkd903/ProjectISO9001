using System.Collections.Generic;
using SuperSchnell.Project.Domain.Rules;

namespace SuperSchnell.Project.Domain
{
    public abstract class Entity<TEntity> : HasId,IEntity
        where TEntity : class, IEntity
    {
        protected static IRulesSet<TEntity> _validationRuleSet =
            new BaseRulesSet<TEntity>();

        protected Entity()
        {
        }

        protected Entity(long id, int version)
            : base(id)
        {
            Version = version;
        }

        public virtual bool IsDeleted { get; private set; }

        public virtual int Version { get; private set; }

        public virtual void Delete()
        {
            IsDeleted = true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (ReferenceEquals(null, obj) ||
                !typeof (TEntity).IsAssignableFrom(obj.GetType()))
            {
                return false;
            }
            return Id.Equals(((TEntity) obj).Id) && Version.Equals(((TEntity) obj).Version);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ 397*Version.GetHashCode() ^ 231;
        }

        public virtual bool IsValid(out IEnumerable<string> errors)
        {
            return _validationRuleSet.UpholdsRules(this as TEntity, out errors);
        }
    }
}