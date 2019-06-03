using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Core.Entity
{
    public interface IEntity<TPrimaryKey> where TPrimaryKey:IComparable
    {
        TPrimaryKey Id { get; set; }
    }
}
