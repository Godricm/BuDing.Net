using BuDing.Core.Entity;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Extensions;
using BuDing.Ioc.UnitOfWork.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork
{
    public abstract partial class Repository<TEntity, TPrimaryKey>
        where TEntity : class
        where TPrimaryKey : IComparable
    {

        public virtual TEntity GetByKey(TPrimaryKey key, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return session.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id=@Id", new { Id = key });
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return session.Get(entity);
        }

        public virtual TEntity GetByKey<TSession>(TPrimaryKey key) where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return GetByKey(key, session);
            }
        }
        public TEntity GetByKey(TPrimaryKey key, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return uow.Connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { Id = key }, uow.Transaction);
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.Get(entity);
        } 
        public virtual async Task<TEntity> GetByKeyAsync(TPrimaryKey key, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return await session.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { Id = key });
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return await GetAsync(entity, session);
        }

        public virtual async Task<TEntity>  GetByKeyAsync<TSession>(TPrimaryKey key) where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return await GetByKeyAsync(key, session);
            }
        }

        public virtual async Task<TEntity> GetByKeyAsync(TPrimaryKey key, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return await uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { Id = key }, uow.Transaction);
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return await uow.GetAsync(entity);
        }

        public virtual TEntity Get(TEntity entity, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return session.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPrimaryKey>)entity).Id });
            }
            return session.Get(entity);
        }

        public virtual TEntity Get<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return Get(entity, session);
            }
        }

        public virtual TEntity Get(TEntity entity, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return uow.Connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPrimaryKey>)entity).Id }, uow.Transaction);
            }
            return uow.Get(entity);
        }

        public virtual async Task<TEntity> GetAsync(TEntity entity, ISession session)
        {

            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return await session.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPrimaryKey>)entity).Id });
            }
            return await session.GetAsync(entity);
        }
        public virtual async Task<TEntity> GetAsync(TEntity entity, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return await uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPrimaryKey>)entity).Id }, uow.Transaction);
            }
            return await uow.GetAsync(entity);
        }
        public virtual async Task<TEntity>  GetAsync<TSession>(TEntity entity) where TSession:class,ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return await GetAsync(entity, session);
            }
        }  
    }
}
