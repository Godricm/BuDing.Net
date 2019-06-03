using System;
using System.Collections.Generic;
using System.Text;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers;
using BuDing.Ioc.UnitOfWork.Interfaces;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.ExampleTests.Repository
{
    public interface INewRepository : IRepository<New, int>
    {

    }
    public class NewRepository:Repository<New,int>,INewRepository
    {
        public NewRepository(IDbFactory factory) : base(factory)
        {
        }
    }
}
