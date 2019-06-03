using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuDing.Ioc.UnitOfWork.Interfaces;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork
{
    public interface IRepository<TEntity, TPrimaryKey>
    where TEntity : class
    where TPrimaryKey : IComparable
    {
        int Count(ISession session);
        int Count(IUnitOfWork uow);
        int Count<TSession>() where TSession : class, ISession;

        Task<int> CountAsync(ISession session);
        Task<int> CountAsync(IUnitOfWork uow);
        Task<int> CountAsync<TSession>() where TSession : class, ISession;

        bool DeleteByKey(TPrimaryKey key, ISession session);
        bool DeleteByKey(TPrimaryKey key, IUnitOfWork uow);
        bool DeleteByKey<TSession>(TPrimaryKey key) where TSession : class, ISession;
        Task<bool> DeleteByKeyAsync(TPrimaryKey key, ISession session);
        Task<bool> DeleteByKeyAsync(TPrimaryKey key, IUnitOfWork uow);
        Task<bool> DeleteByKeyAsync<TSession>(TPrimaryKey key) where TSession : class, ISession;

        bool Delete(TEntity entity, ISession session);
        bool Delete(TEntity entity, IUnitOfWork uow);
        bool Delete<TSession>(TEntity entity) where TSession : class, ISession;
        Task<bool> DeleteAsync(TEntity entity, ISession session);
        Task<bool> DeleteAsync(TEntity entity, IUnitOfWork uow);
        Task<bool> DeleteAsync<TSession>(TEntity entity) where TSession : class, ISession;

        TEntity GetByKey(TPrimaryKey key, ISession session);
        TEntity GetByKey(TPrimaryKey key, IUnitOfWork uow);
        TEntity GetByKey<TSession>(TPrimaryKey key) where TSession : class, ISession;
        Task<TEntity> GetByKeyAsync(TPrimaryKey key, ISession session);
        Task<TEntity> GetByKeyAsync(TPrimaryKey key, IUnitOfWork uow);
        Task<TEntity> GetByKeyAsync<TSession>(TPrimaryKey key) where TSession : class, ISession;

        TEntity Get(TEntity entity, ISession session);
        TEntity Get(TEntity entity, IUnitOfWork uow);
        TEntity Get<TSession>(TEntity entity) where TSession : class, ISession;
        Task<TEntity> GetAsync(TEntity entity, ISession session);
        Task<TEntity> GetAsync(TEntity entity, IUnitOfWork uow);
        Task<TEntity> GetAsync<TSession>(TEntity entity) where TSession : class, ISession;
         
        IEnumerable<TEntity> GetAll(ISession session);
        IEnumerable<TEntity> GetAll(IUnitOfWork uow);
        IEnumerable<TEntity> GetAll<TSession>() where TSession : class, ISession;
        Task<IEnumerable<TEntity>> GetAllAsync(ISession session);
        Task<IEnumerable<TEntity>> GetAllAsync(IUnitOfWork uow);
        Task<IEnumerable<TEntity>> GetAllAsync<TSession>() where TSession : class, ISession;

        TPrimaryKey SaveOrUpdate(TEntity entity, IUnitOfWork uow);
        TPrimaryKey SaveOrUpdate<TSession>(TEntity entity) where TSession : class, ISession;

        Task<TPrimaryKey> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow);
        Task<TPrimaryKey> SaveOrUpdateAsync<TSession>(TEntity entity) where TSession : class, ISession;
    }
}
