using System;
using System.Collections.Generic;
using System.Text;
using BuDing.Entity;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    public class World:IEntity<int>
    {
        public int Id { get; set; }
        public string Guid { get; set; }
    }
}
