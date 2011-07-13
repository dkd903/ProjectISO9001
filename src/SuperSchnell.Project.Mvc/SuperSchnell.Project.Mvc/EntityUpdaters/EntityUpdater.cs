using System.Collections.Generic;
using NHibernate;
using SuperSchnell.Project.Domain;

namespace SuperSchnell.Project.Mvc.EntityUpdaters
{
    public abstract class EntityUpdater<TEntity> : IEntityUpdater<TEntity>
        where TEntity : class, IEntity
    {
        protected TEntity _entity;

        public void SetOldEntity(TEntity entity)
        {
            _entity = entity;
        }

        public abstract bool Update(ISession session, out IEnumerable<string> errors);
        public abstract long Id { get; }
        public abstract int Version { get; }
        public abstract TEntity CreateNewEntity(ISession session);
    }
}