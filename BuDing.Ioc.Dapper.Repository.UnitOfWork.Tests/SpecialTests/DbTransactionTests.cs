using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.ExampleTests.Repository;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class DbTransactionTests:CommonTestDataSetup
    {
        [Test]
        public void Rollback_DoesNotThrow_OnDisposalAfterAlreadyBeingCalled()
        {
            var repo = new BraveRepository(Factory);
            Assert.DoesNotThrow(() => {
                using(var uow = Connection.UnitOfWork(IsolationLevel.Serializable))
                {
                    var result = repo.SaveOrUpdate(new Brave { NewId = 3 }, uow);
                    result.Should().BePositive();
                    uow.Rollback();
                }
            });
        }
    }
}
