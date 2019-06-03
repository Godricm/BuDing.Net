using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    public interface IMyDatabaseSettings
    {
        string ConnectionString { get; }
    }
}
