using Dapper.FastCrud;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BuDing.Extensions;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    [TestFixture]
    public class EnumHelperTests
    {
        [Test]
        public void ConvertIntToEnum_Converts_FromInteger()
        {
            var actual = EnumExtensions.ConvertIntToEnum<SqlDialect>(1);
            actual.Should().Be(SqlDialect.MySql);
        }

        [Test]
        public void ConvertEnumToEnum_Converts_FromEnum()
        {
            var actual = EnumExtensions.ConvertEnumToEnum<SqlDialect>(Ioc.UnitOfWork.SqlDialect.PostgreSql);
            actual.Should().Be(SqlDialect.PostgreSql);
        }
    }
}
