using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuDing.Core.Entity
{
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey> where TPrimaryKey : IComparable
    {
        [Key]
        public TPrimaryKey Id { get; set; }
    }
}
