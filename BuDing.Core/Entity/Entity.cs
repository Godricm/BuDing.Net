using System;
using System.ComponentModel.DataAnnotations;
using Dapper.FastCrud;

namespace BuDing.Entity
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey> where TPrimaryKey : IComparable
    {
        [Key]
        [DatabaseGeneratedDefaultValue]
        public TPrimaryKey Id { get; set; }
    }
}
