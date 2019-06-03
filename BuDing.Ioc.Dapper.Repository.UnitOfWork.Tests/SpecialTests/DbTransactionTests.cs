using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class DbTransactionTests:CommonTestDataSetup
    {
        [Test]
        public void Rollback_DoesNotThrow_OnDisposalAfterAlreadyBeingCalled()
        {
           
        }
    }
}
