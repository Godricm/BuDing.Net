using System;
using System.Collections.Generic;
using System.Text;
using BuDing.Extensions;
using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Helpers
{
    public class SqlInstance
    {
        private static volatile SqlInstance _instance;

        private static readonly object SyncRoot = new object();
        private readonly SqlDialectHelper _sqlDialectHelper = new SqlDialectHelper();

        private SqlInstance() { }

        public static SqlInstance Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    if (_instance == null)
                        _instance = new SqlInstance();
                }
                return _instance;
            }
        }

        #region Column

        public IFormattable Column<TEntity>(string propertyName, EntityMapping entityMappingOverride = null)
            where TEntity : class
        {
            return Sql.Column<TEntity>(propertyName, entityMappingOverride);
        }

        public IFormattable Column<TEntity>(Ioc.UnitOfWork.SqlDialect sqlDialect, string propertyName,
            EntityMapping entityMappingOverride = null) where TEntity : class
        {
            return Column<TEntity>(EnumExtensions.ConvertEnumToEnum<SqlDialect>(sqlDialect), propertyName, entityMappingOverride);
        }

        public IFormattable Column<TEntity>(SqlDialect sqlDialect, string propertyName, EntityMapping entityMappingOverride = null) where TEntity : class
        {
            _sqlDialectHelper.SetDialogueIfNeeded<TEntity>(sqlDialect);
            return Sql.Column<TEntity>(propertyName, entityMappingOverride);
        }



        #endregion

        #region Table


        public IFormattable Table<TEntity>(EntityMapping entityMappingOverride = null) where TEntity : class
        {
            return Sql.Table<TEntity>(entityMappingOverride);
        }

        public IFormattable Table<TEntity>(Ioc.UnitOfWork.SqlDialect sqlDialect, EntityMapping entityMappingOverride = null) where TEntity : class
        {
            return Table<TEntity>(EnumExtensions.ConvertEnumToEnum<SqlDialect>(sqlDialect), entityMappingOverride);
        }

        public IFormattable Table<TEntity>(SqlDialect sqlDialect, EntityMapping entityMappingOverride = null) where TEntity : class
        {
            _sqlDialectHelper.SetDialogueIfNeeded<TEntity>(sqlDialect);
            return Sql.Table<TEntity>(entityMappingOverride);
        }
        #endregion

        #region TableAndColumn

        public IFormattable TableAndColumn<TEntity>(string propertyName,
            EntityMapping entityMappingOverride = null) where TEntity : class
        {
            return Sql.TableAndColumn<TEntity>(propertyName, entityMappingOverride);
        }

        public IFormattable TableAndColumn<TEntity>(Ioc.UnitOfWork.SqlDialect sqlDialect, string propertyName,
            EntityMapping entityMappingOverride = null) where TEntity : class
        {
            return TableAndColumn<TEntity>(EnumExtensions.ConvertEnumToEnum<SqlDialect>(sqlDialect), propertyName,
                entityMappingOverride);
        }

        public IFormattable TableAndColumn<TEntity>(SqlDialect sqlDialect, string propertyName,
            EntityMapping entityMappingOverride = null) where TEntity : class
        {
            _sqlDialectHelper.SetDialogueIfNeeded<TEntity>(sqlDialect);
            return Sql.TableAndColumn<TEntity>(propertyName, entityMappingOverride);
        }


        #endregion

    }
}
