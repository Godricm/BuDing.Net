using System.Data;

namespace BuDing.Ioc.UnitOfWork.Interfaces
{
    public interface ISession:IDbConnection
    {
        IDbConnection Connection { get; }

        IUnitOfWork UnitOfWork();

        IUnitOfWork UnitOfWork(IsolationLevel isolationLevel);

        SqlDialect SqlDialect { get; }
    }
}
