using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SuperSchnell.Project.Domain;
using SuperSchnell.Project.Mvc.EntityUpdaters;
using SuperSchnell.Project.Mvc.Utilities;

namespace SuperSchnell.Project.Mvc.Repositories
{
    public class NHibernateRepository:IRepository
    {
        private static readonly ISessionFactory _sessionFactory;
        static NHibernateRepository()
        {
            var configuration = new Configuration().Configure();
            //Comment this line in to create the DB
            //new SchemaExport(configuration).Execute(true, true, false);
            _sessionFactory = configuration.BuildSessionFactory();
        }

        public SaveResult TrySaveUpdate<TEntity>(IEntityUpdater<TEntity> entityUpdater)
            where TEntity : class, IEntity
        {
            return entityUpdater.Id != 0 ? TryUpdate(entityUpdater) : TrySave(entityUpdater);
        }

        public T WrapQueryInTransaction<T>(Func<ISession, T> query)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        var result = query(session);
                        tx.Commit();
                        return result;
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        //Handle logging
                        throw;
                    }
                }
            }
        }

        public SaveResult TryDelete<TEntity>(long id, int version) 
            where TEntity : class, IEntity
        {
            if (id == 0)
            {
                return SaveResult.ErrorResult(
                    new[] { "SuperSchnell_Project_Mvc_Utilities_SaveResult_TransientEntity_Cannot_Be_Deleted" });
            }
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        var entity = session.Get<TEntity>(id);
                        if (entity.Version != version)
                        {
                            tx.Rollback();
                            return SaveResult.ConcurrencyConflictResult();
                        }
                        entity.Delete();
                        session.Update(entity);
                        tx.Commit();
                     
                        return SaveResult.SuccessResult();
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        private SaveResult TrySave<TEntity>(IEntityUpdater<TEntity> entityUpdater)
            where TEntity : class, IEntity
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        IEnumerable<string> errors;
                        var entity = entityUpdater.CreateNewEntity(session);
                        entityUpdater.SetOldEntity(entity);
                        if (!entityUpdater.Update(session, out errors) ||
                            !entity.IsValid(out errors))
                        {
                            return SaveResult.ErrorResult(errors);
                        }
                        session.Save(entity);
                        tx.Commit();
                        return SaveResult.SuccessResult(entity.Id);
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        //TODO: Log exception
                        throw;
                    }
                }
            }
        }

        private SaveResult TryUpdate<TEntity>(IEntityUpdater<TEntity> entityUpdater)
            where TEntity : class, IEntity
        {
            if (entityUpdater.Id == 0)
            {
                return SaveResult.NotFoundResult();
            }
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        var entity = session.Get<TEntity>(entityUpdater.Id);
                        if (entity == null)
                        {
                            return SaveResult.NotFoundResult();
                        }
                        if (entity.Version != entityUpdater.Version)
                        {
                            return SaveResult.ConcurrencyConflictResult();
                        }
                        entityUpdater.SetOldEntity(entity);
                        IEnumerable<string> errors;
                        if (!entityUpdater.Update(session, out errors) ||
                            !entity.IsValid(out errors))
                        {
                            return SaveResult.ErrorResult(errors);
                        }
                        tx.Commit();

                        return SaveResult.SuccessResult(entity.Id);
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}