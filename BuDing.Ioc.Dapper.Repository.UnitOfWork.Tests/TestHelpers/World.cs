using BuDing.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    public class World:IEntity<int>
    {
        public int Id { get; set; }
        public string Guid { get; set; }
    }
}
