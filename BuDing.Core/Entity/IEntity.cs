using System;

namespace BuDing.Entity
{
    public interface IEntity<TPrimaryKey> where TPrimaryKey:IComparable
    {
        TPrimaryKey Id { get; set; }
    }
}
