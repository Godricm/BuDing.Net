using BuDing.Core.Entity;
using BuDing.Ioc.UnitOfWork.Abstractions;
using BuDing.Ioc.UnitOfWork.Interfaces;
using FakeItEasy;
using NUnit.Framework;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.SpecialTests
{
    [TestFixture]
    public class MsSqlGuidTests
    {
        private static TestSqlForGuid TestSession;
        private const string DbName = "TestMsSql";

        [SetUp]
        public void TestSetup()
        {
            if (TestSession != null) return;
            if (!File.Exists(DbName)) using (File.Create(DbName)) { }
            TestSession = new TestSqlForGuid(A.Fake<IDbFactory>());
            var migrator = new SimpleMigrator(Assembly.GetExecutingAssembly(), new MssqlDatabaseProvider(TestSession.Connection as SqlConnection));
            migrator.Load();
            migrator.MigrateToLatest();
        }

        [Test,Category("IntegrationMssqlce"),Explicit("Needs Sql")]
        public void Insert_Returns_IdAsGuid()
        {
            
        }


        [Table("FooGuidTest")]
        class FooGuidTest
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            public string Something { get; set; }
        }

        class FooGuidTestWithIEntity : IEntity<Guid>
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            public string Something { get; set; }
        }


        private class TestSqlForGuid : Session<SqlConnection>, ITestSqlForGuid
        {
            public TestSqlForGuid(IDbFactory factory) : base(factory, $@"Server=.\SQLEXPRESS;Database={DbName};Trusted_Connection=True;")
            {
            }
        }

        interface ITestSqlForGuid : ISession
        {
        }
    }
}
