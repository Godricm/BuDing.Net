using BuDing.Core.Entity;
using BuDing.Ioc.UnitOfWork.Abstractions;
using BuDing.Ioc.UnitOfWork.Interfaces;
using Dapper;
using Dapper.FastCrud;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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

        [Test, Category("IntegrationMssqlce"), Explicit("Needs Sql")]
        public void Insert_Returns_IdAsGuid()
        {
            var foo = new FooGuidTest { Something = "bar 1" };
            TestSession.Execute("DELETE FROM FooGuidTest");
            TestSession.Insert(foo);
            var actual = TestSession.Find<FooGuidTest>();
            actual.Should().HaveCount(x => x > 0);
            actual.First().Id.Should().NotBe(new Guid());
        }

        [Test, Category("IntegrationMssqlce"), Explicit("Needs Sql")]
        public void SaveAndUpdate_Returns_AsGuid()
        {
            var foo = new FooGuidTest { Something = "bar 1" };
            TestSession.Execute("Delete From FooGuidTest");
            var repo = new FooRepo1(A.Fake<IDbFactory>());
            using (var uow = new Ioc.UnitOfWork.UnitOfWork(A.Fake<IDbFactory>(), TestSession))
            {
                repo.SaveOrUpdate(foo, uow);
            }
            var actual = repo.GetAll(TestSession);
            actual.Should().HaveCount(x => x > 0);
            actual.First().Id.Should().NotBe(new Guid());
        }

        [Test, Category("IntegrationMssqlce"), Explicit("Needs Sql")]
        public void SaveAndUpdate_Returns_dAsGuidWhereEntityIsIEntity()
        {
            var foo = new FooGuidTestWithIEntity { Something = "bar 1" };
            TestSession.Execute("DELETE FROM FooGuidTestWithIEntity");
            var repo = new FooRepo2(A.Fake<IDbFactory>());
            using(var uow=new Ioc.UnitOfWork.UnitOfWork(A.Fake<IDbFactory>(), TestSession))
            {
                repo.SaveOrUpdate(foo, uow);
            }
            var actual = repo.GetAll(TestSession);
            actual.Should().HaveCount(x => x > 0);
            actual.First().Id.Should().NotBe(new Guid()); 
        }

        [Table("FooGuidTest")]
        class FooGuidTest
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            public string Something { get; set; }
        }

        class CreateFoo : Migration
        {
            protected override void Down()
            {
                Execute("DROP TABLE FooGuidTest");
            }

            protected override void Up()
            {
                if (!Connection.ConnectionString.Contains(DbName)) return;
                Execute(@"CREATE TABLE FooGuidTest(Id UNIQUEIDENTIFIER DEFAULT NEWID(),Something VARCHAR(20));");
                Execute(@"CREATE TABLE FooGuidTestWithIEntity(Id UNIQUEIDENTIFIER DEFAULT NEWID(),Something VARCHAR(20));");
            }
        }

        class FooRepo1 : Repository<FooGuidTest, Guid>, IRepository<FooGuidTest, Guid>
        {
            public FooRepo1(IDbFactory factory) : base(factory)
            {
            }
        }

        class FooGuidTestWithIEntity : IEntity<Guid>
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            public string Something { get; set; }
        }

        class FooRepo2 : Repository<FooGuidTestWithIEntity, Guid>, IRepository<FooGuidTestWithIEntity, Guid>
        {
            public FooRepo2(IDbFactory factory) : base(factory)
            {
            }
        }

        private class TestSqlForGuid : Session<SqlConnection>, ITestSqlForGuid
        {
            public TestSqlForGuid(IDbFactory factory) : base(factory, $@"Data Source=(localdb)\MSSQLLocalDB;Database={DbName};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            {
            }
        }

        interface ITestSqlForGuid : ISession
        {
        }
    }
}
