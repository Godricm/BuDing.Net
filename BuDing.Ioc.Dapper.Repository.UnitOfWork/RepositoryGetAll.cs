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



        public virtual IEnumerable<TEntity> GetAll(ISession session)
        {
            return _container.IsIEntity<TEntity, TPrimaryKey>() ?
              session.Query<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)}")
              : session.Find<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(IUnitOfWork uow)
        {
            return _container.IsIEntity<TEntity, TPrimaryKey>() ?
               uow.Connection.Query<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)}", transaction: uow.Transaction)
               : uow.Find<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll<TSession>() where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return GetAll(session);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(ISession session)
        {
            return _container.IsIEntity<TEntity, TPrimaryKey>() ?
               await session.QueryAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(session.SqlDialect)}")
               : await session.FindAsync<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(IUnitOfWork uow)
        {
            return _container.IsIEntity<TEntity, TPrimaryKey>() ?
                    await uow.Connection.QueryAsync<TEntity>($"SELECT * FROM {Sql.Table<TEntity>(uow.SqlDialect)}", transaction: uow.Transaction)
                    : await uow.FindAsync<TEntity>();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TSession>() where TSession : class, ISession
        {
            using (var session = Factory.Create<TSession>())
            {
                return await GetAllAsync(session);
            }
        } 
    }
}
