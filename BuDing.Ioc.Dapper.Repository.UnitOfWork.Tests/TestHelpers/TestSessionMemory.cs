using BuDing.Ioc.UnitOfWork.Abstractions;
using BuDing.Ioc.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    public interface ITestSessionMemory : ISession
    {

    }

    public class TestSessionMemory : Session<SQLiteConnection>, ITestSessionMemory
    {
        public TestSessionMemory(IDbFactory factory)
            : base(factory, "Data Source=:memory:;Version=3;New=True;")
        {
        }
    }
}
