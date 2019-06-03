using System;
using System.Collections.Generic;
using System.Text;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers;
using Dapper;
using NUnit.Framework;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    [TestFixture]
    public class RepositoryQueryTests : CommonTestDataSetup
    {
        [Test, Category("Integration")]
        public static void Query_Returns_DataFromBrave()
        {
            IEnumerable<Brave> results = null;
            Assert.DoesNotThrow(() => results = Connection.Query<Brave>("Select * FROM Braves"));
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Not.Empty);
        }
    }
}
