using System.Collections.Generic;
using NHibernate;
using SuperSchnell.Project.Domain;

namespace SuperSchnell.Project.Mvc.EntityUpdaters
{
    public interface IEntityUpdater<TEntity>
        where TEntity : class,IEntity
    {
        void SetOldEntity(TEntity entity);
        bool Update(ISession session, out IEnumerable<string> errors);
        long Id { get; }
        int Version { get; }
        TEntity CreateNewEntity(ISession session);
    }
}