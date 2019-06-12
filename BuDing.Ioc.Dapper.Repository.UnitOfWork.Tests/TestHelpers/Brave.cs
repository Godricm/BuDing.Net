using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BuDing.Entity;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    public class Brave:Entity<int>
    {
        [ForeignKey("New")]
        public int NewId { get; set; }
        public New New { get; set; }
    }
}
