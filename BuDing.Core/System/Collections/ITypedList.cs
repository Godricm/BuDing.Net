using System;
using System.Collections;
using System.Collections.Generic;

namespace BuDing.System.Collections
{
    public interface ITypedList : ITypedList<object>
    {

    }

    public interface ITypedList<in TBaseType>:IList<Type>
    {
        void Add<T>() where T: TBaseType;

        void TryAdd<T>() where T : TBaseType;

        void Contains<T>() where T : TBaseType;

        void Remove<T>() where T : TBaseType;
    }
}
