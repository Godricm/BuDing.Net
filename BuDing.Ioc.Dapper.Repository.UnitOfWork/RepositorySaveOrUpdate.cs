using BuDing.Core.Reflection;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Extensions;
using BuDing.Ioc.UnitOfWork.Interfaces;
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

        public virtual TPrimaryKey SaveOrUpdate(TEntity entity, IUnitOfWork uow)
        {
            if (TryAllKeysDefault(entity))
            {
                uow.Insert(entity);
            }
            else
            {
                uow.Update(entity);
            }

            var primaryKeyValue = GetPrimaryKeyValue(entity);
            return primaryKeyValue != null ? primaryKeyValue : default(TPrimaryKey);
        }

        public virtual TPrimaryKey SaveOrUpdate<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return SaveOrUpdate(entity, uow);
            }
        }

        public virtual async Task<TPrimaryKey> SaveOrUpdateAsync(TEntity entity, IUnitOfWork uow)
        {
            return await Task.Run(() =>
            {
                if (TryAllKeysDefault(entity))
                {
                    uow.InsertAsync(entity);
                }
                else
                {
                    uow.UpdateAsync(entity);
                }
                var primaryKeyValue = GetPrimaryKeyValue(entity);
                return primaryKeyValue != null ? primaryKeyValue : default(TPrimaryKey);
            });
        }



        public virtual async Task<TPrimaryKey> SaveOrUpdateAsync<TSession>(TEntity entity) where TSession : class, ISession
        {
            using (var uow = Factory.Create<IUnitOfWork, TSession>())
            {
                return await SaveOrUpdateAsync(entity, uow);
            }
        }
    }
}
