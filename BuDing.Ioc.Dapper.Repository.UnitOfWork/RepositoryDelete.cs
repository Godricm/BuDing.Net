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
        public virtual bool DeleteByKey(TPrimaryKey key, ISession session)
        {
            var entity = CreateEntityAndSetKeyValue(key);
            return session.Delete(entity);
        }

        public virtual bool DeleteByKey(TPrimaryKey key, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return uow.Connection.Execute(
                    $"DEELET FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id=@id",
                    new { Id = key }, uow.Transaction
                    ) == 1;
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return uow.Delete(entity);
        }
        public virtual bool DeleteByKey<TSession>(TPrimaryKey key) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return DeleteByKey(key, uow);
            }
        }


        public virtual async Task<bool> DeleteByKeyAsync(TPrimaryKey key, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return await Task.Run(() => session.Execute($"DELETE FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id=@Id",
                            new { Id = key }
                            )) == 1;
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return await session.DeleteAsync(entity);
        }

        public virtual async Task<bool> DeleteByKeyAsync(TPrimaryKey key, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return await Task.Run(() => uow.Connection.Execute($"DELETE FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id=@Id",
                            new { Id = key }, uow.Transaction
                            )) == 1;
            }
            var entity = CreateEntityAndSetKeyValue(key);
            return await uow.DeleteAsync(entity);
        }

        public virtual async Task<bool> DeleteByKeyAsync<TSession>(TPrimaryKey key) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return await DeleteByKeyAsync(key, uow);
            }
        }
        public virtual bool Delete(TEntity entity, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return session.Execute($"DELETE FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPrimaryKey>)entity).Id }) == 1;
            }
            return session.Delete(entity);
        }

        public virtual bool Delete(TEntity entity, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return uow.Connection.Execute($"DELETE FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                    new { ((IEntity<TPrimaryKey>)entity).Id }, uow.Transaction) == 1;
            }
            return uow.Delete(entity);
        }


        public virtual bool Delete<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                if (_container.IsIEntity<TEntity, TPrimaryKey>())
                {
                    return uow.Connection.Execute($"DELETE FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                        new { ((IEntity<TPrimaryKey>)entity).Id }, uow.Transaction) == 1;
                }
                return uow.Delete(entity);
            }
        } 

        public virtual async Task<bool> DeleteAsync(TEntity entity, ISession session)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return await Task.Run(() => session.Execute(
                            $"DELETE FROM {Sql.Table<TEntity>(session.SqlDialect)} WHERE Id = @Id",
                            new { ((IEntity<TPrimaryKey>)entity).Id }) == 1);
            }
            return await session.DeleteAsync(entity);
        }

        
        public virtual Task<bool> DeleteAsync(TEntity entity, IUnitOfWork uow)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                return Task.Run(() => uow.Connection.Execute(
                                $"DELETE FROM {Sql.Table<TEntity>(uow.SqlDialect)} WHERE Id = @Id",
                                new { ((IEntity<TPrimaryKey>)entity).Id }, uow.Transaction) == 1);
            }
            return uow.DeleteAsync(entity);
        }
         
        public virtual async Task<bool> DeleteAsync<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return await DeleteAsync(entity, uow);
            }
        }

    }

}
