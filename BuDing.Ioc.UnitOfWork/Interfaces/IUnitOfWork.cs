using System;
using System.Data;

namespace BuDing.Ioc.UnitOfWork.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        SqlDialect SqlDialect { get; }

        IDbTransaction Transaction { get; set; }

        /// <summary>
        /// 数据源的已打开连接，
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 事务级别
        /// </summary>
        IsolationLevel IsolationLevel { get; }

        void Commit();

        void Rollback();
    }
}
