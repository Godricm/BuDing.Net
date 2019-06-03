using SimpleMigrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers.Migrations
{
    [Migration(2)]
    public class AddDataBraveNewWorld : Migration
    {
        protected override void Down()
        {
            Execute("DROP TABLE Brave");
            Execute("DROP TABLE New");
            Execute("DROP TABLE World");
        }

        protected override void Up()
        {
            if (!Connection.ConnectionString.Contains("RepoTests.db") && !Connection.ConnectionString.Contains(":memory:;")) return;
            Execute($@"INSERT INTO Worlds (Guid) VALUES ('{Guid.NewGuid()}');");
            Execute($@"INSERT INTO Worlds (Guid) VALUES ('{Guid.NewGuid()}');");
            Execute($@"INSERT INTO Worlds (Guid) VALUES ('{Guid.NewGuid()}');");

            Execute($@"INSERT INTO News (WorldId) VALUES (2);");
            Execute($@"INSERT INTO News (WorldId) VALUES (3);");
            Execute($@"INSERT INTO News (WorldId) VALUES (1);");

            Execute($@"INSERT INTO Braves (NewId) VALUES (3);");
            Execute($@"INSERT INTO Braves (NewId) VALUES (1);");
            Execute($@"INSERT INTO Braves (NewId) VALUES (2);");
        }
    }
}
