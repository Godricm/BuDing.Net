using BuDing.Ioc.UnitOfWork.Interfaces;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers.Migrations
{
    public class MigrateDb
    {
        public MigrateDb(ISession connection)
        {
            var migrationsAssembly = Assembly.GetExecutingAssembly();
            var migrator= new SimpleMigrator(migrationsAssembly, new SqliteDatabaseProvider(connection.Connection as SQLiteConnection));
            migrator.Load();
            migrator.MigrateToLatest();
        }
    }
}
