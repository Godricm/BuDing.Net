using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    [NoIoCFluentRegistration]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class NoIoCFluentRegistration : Attribute
    {
    }
}
