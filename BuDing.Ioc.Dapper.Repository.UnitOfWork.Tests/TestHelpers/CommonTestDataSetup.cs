using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers.Migrations;
using BuDing.Ioc.UnitOfWork.Interfaces;
using FakeItEasy;
using FakeItEasy.Core;
using NUnit.Framework;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    public abstract class CommonTestDataSetup
    {
        private static IMyDatabaseSettings _settings;
        
        public  static  ITestSession Connection { get; private set; }

        public static  IDbFactory Factory { get; private set; }

        [SetUp]
        public static void TestSetup()
        {
            if (Connection != null) return;
            Factory = A.Fake<IDbFactory>(x => x.Strict());
            _settings = A.Fake<IMyDatabaseSettings>();
            var path = $@"{TestContext.CurrentContext.TestDirectory}\RepoTests.db";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            A.CallTo(() => _settings.ConnectionString)
                .Returns($@"Data Source={path};Version=3;New=True;BinaryGUID=False;");
            A.CallTo(() => Factory.Create<ITestSession>()).ReturnsLazily(CreateSession);
            A.CallTo(() => Factory.Create<ISession>()).ReturnsLazily(CreateSession);
            A.CallTo(() => Factory.Create<IUnitOfWork>(A<IDbFactory>._, A<ISession>._, IsolationLevel.Serializable))
                .ReturnsLazily(CreateUnitOfWork);
            A.CallTo(() => Factory.Create<IUnitOfWork, ITestSession>(A<IsolationLevel>._))
                .ReturnsLazily(CreateUnitOfWork);
            A.CallTo(() => Factory.Create<IUnitOfWork, ISession>(A<IsolationLevel>._)).ReturnsLazily(CreateUnitOfWork);
            A.CallTo(() => Factory.Release(A<IDisposable>._)).DoesNothing();
            new MigrateDb(Connection); 
        }

        internal static ITestSession CreateSession(IFakeObjectCall arg)
        {
            return new TestSession(Factory,_settings);
        }

        private static IUnitOfWork CreateUnitOfWork(IFakeObjectCall arg)
        {
            return new Ioc.UnitOfWork.UnitOfWork((IDbFactory)arg.FakedObject,CreateSession(null),IsolationLevel.Serializable);
        }
    }
}
