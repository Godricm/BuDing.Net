using Dapper.FastCrud;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuDing.Core.Entity
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey> where TPrimaryKey : IComparable
    {
        [Key]
        [DatabaseGeneratedDefaultValue]
        public TPrimaryKey Id { get; set; }
    }
}
