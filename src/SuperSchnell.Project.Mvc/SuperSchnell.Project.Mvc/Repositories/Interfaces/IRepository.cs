using System;
using NHibernate;
using SuperSchnell.Project.Domain;
using SuperSchnell.Project.Mvc.EntityUpdaters;
using SuperSchnell.Project.Mvc.Utilities;

namespace SuperSchnell.Project.Mvc.Repositories
{
    public interface IRepository
    {
        SaveResult TrySaveUpdate<TEntity>(IEntityUpdater<TEntity> entityUpdater)
            where TEntity : class, IEntity;
        SaveResult TryDelete<TEntity>(long id, int version)
            where TEntity : class, IEntity;
        T WrapQueryInTransaction<T>(Func<ISession, T> query);
    }
}