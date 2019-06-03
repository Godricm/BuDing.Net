using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers;
using BuDing.Ioc.UnitOfWork.Interfaces;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class DbCastTests
    {
        [Test, Category("Integration")]
        public static void ISession_Is_IDbConnectionAndIsConnected()
        {
            using(var session=new TestSessionMemory(A.Fake<IDbFactory>()))
            {
                Assert.That(session.State, Is.EqualTo(ConnectionState.Open));
                var connection = (IDbConnection)session;
                Assert.That(connection.State, Is.EqualTo(ConnectionState.Open));
            }

            using (IDbConnection session = new TestSessionMemory(A.Fake<IDbFactory>()))
            {
                Assert.That(session.State, Is.EqualTo(ConnectionState.Open));
            }
        }
    }
}
