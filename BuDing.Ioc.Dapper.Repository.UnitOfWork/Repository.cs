using BuDing.Ioc.Dapper.Repository.UnitOfWork.Containers;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Helpers;
using BuDing.Ioc.UnitOfWork.Abstractions;
using BuDing.Ioc.UnitOfWork.Exceptions;
using BuDing.Ioc.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BuDing.Entity;
using BuDing.Reflection;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork
{
    public abstract partial class Repository<TEntity, TPrimaryKey> : RepositoryBase, IRepository<TEntity, TPrimaryKey>
        where TEntity:class
        where TPrimaryKey:IComparable
    {

        private readonly RepositoryContainer _container = RepositoryContainer.Instance;
        private readonly SqlDialectHelper _helper;

        protected SqlInstance Sql { get; } = SqlInstance.Instance;

        protected Repository(IDbFactory factory) : base(factory)
        {
            _helper = new SqlDialectHelper();
        }

        protected bool TryAllKeysDefault(TEntity entity)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                if(entity is IEntity<TPrimaryKey> entityInterface)
                {
                    return entityInterface.Id.CompareTo(default(TPrimaryKey)) == 0;
                }
            }

            var keys = _container.GetKeys<TEntity>();
            var properties = _container.GetProperties<TEntity>(keys);
            if (keys == null || properties == null)
            {
                throw new NoPkException(
                  "There is no keys for this entity, please create your logic or add a key attribute to the entity");
            }
            return properties.Select(property => property.GetValue(entity)).All(value => value == null || value.Equals(default(TPrimaryKey)));
        }

        protected TPrimaryKey GetPrimaryKeyValue(TEntity entity)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                if(entity is IEntity<TPrimaryKey> entityInterface)
                {
                    return entityInterface.Id;
                }
            }
            var primaryKeyValue = GetPrimaryKeyPropertyInfo();
            return (TPrimaryKey)primaryKeyValue.GetValue(entity);
        }

        protected void SetPrimaryKeyValue(TEntity entity,TPrimaryKey value)
        {
            if (_container.IsIEntity<TEntity, TPrimaryKey>())
            {
                if(entity is IEntity<TPrimaryKey> entityInterface)
                {
                    entityInterface.Id = value;
                    return;
                }
            }
            var primaryKeyValue = GetPrimaryKeyPropertyInfo();
            primaryKeyValue.SetValue(entity, value);
        }

        private PropertyInfo GetPrimaryKeyPropertyInfo()
        {
            var keys = _container.GetKeys<TEntity>();
            var primaryKeyName = keys.FirstOrDefault(key => key.IsPrimaryKey)?.PropertyName;
            var properties = _container.GetProperties<TEntity>(keys);
            if (keys == null || primaryKeyName == null || properties == null)
            {
                throw new NoPkException(
                 "There is no primary ket for this entity, please create your logic or add a key attribute to the entity");
            }

            var primaryKeyValue = properties.FirstOrDefault(property => property.Name.Equals(primaryKeyName, StringComparison.Ordinal));
            return primaryKeyValue;
        }
         
        protected TEntity CreateEntityAndSetKeyValue(TPrimaryKey key)
        {
            var entity = CreateInstanceHelper.Resolve<TEntity>();
            SetPrimaryKeyValue(entity, key);
            return entity;
        }
         
        protected void SetDialogueIfNeeded<T>(IUnitOfWork uow) where T : class
        {
            _helper.SetDialogueIfNeeded<T>(uow.SqlDialect);
        }

        protected void SetDialogueOnce<T>(ISession session) where T : class
        {
            _helper.SetDialogueIfNeeded<T>(session.SqlDialect);
        }
    }
}
