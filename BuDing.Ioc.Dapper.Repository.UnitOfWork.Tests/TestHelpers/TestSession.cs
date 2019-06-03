using BuDing.Ioc.UnitOfWork.Abstractions;
using BuDing.Ioc.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{

    public interface ITestSession : ISession
    {

    }
    public class TestSession : Session<SQLiteConnection>, ITestSession
    {
        public TestSession(IDbFactory session,IMyDatabaseSettings settings):base(session,settings.ConnectionString)
        {
        }
    }
}
