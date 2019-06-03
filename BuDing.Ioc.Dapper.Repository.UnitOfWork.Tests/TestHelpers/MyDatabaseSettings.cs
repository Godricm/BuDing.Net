using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    public class MyDatabaseSettings : IMyDatabaseSettings
    {
        public string ConnectionString =>  $@"Data Source={TestContext.CurrentContext.TestDirectory}\IoCTests.db;Version=3;New=True;BinaryGUID=False;";
    }
}
