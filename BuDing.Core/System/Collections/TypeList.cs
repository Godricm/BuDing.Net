using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BuDing.System.Collections
{

    public class TypeList : TypeList<object>, ITypeList
    {

    }

    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        public TypeList()
        {
            _typeList = new List<Type>();
        }

        private readonly List<Type> _typeList;


        public int Count => _typeList.Count;

        public IEnumerator<Type> GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        public void Add(Type item)
        {
            CheckType(item);
            _typeList.Add(item);
        }

        public void Clear()
        {
            _typeList.Clear();
        }

        public bool Contains(Type item)
        {
            return _typeList.Contains(item);
        }

        public void CopyTo(Type[] array, int arrayIndex)
        {
            _typeList.CopyTo(array,arrayIndex);
        }

        public bool Remove(Type item)
        {
           return _typeList.Remove(item);
        }

        public bool IsReadOnly => false;
        public int IndexOf(Type item)
        {
            return _typeList.IndexOf(item);
        }

        public void Insert(int index, Type item)
        {
            CheckType(item);
            _typeList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _typeList.RemoveAt(index);
        }

        public Type this[int index]
        {
            get => _typeList[index];
            set
            {
                CheckType(value);
                _typeList[index] = value;
            }
        }

        public void Add<T>() where T : TBaseType
        {
            _typeList.Add(typeof(T));
        }

        public void TryAdd<T>() where T : TBaseType
        {
            if (Contains<T>())
            {
                return;
            }
            Add<T>();
        }

        public bool Contains<T>() where T : TBaseType
        {
            return _typeList.Contains(typeof(T));
        }

        public void Remove<T>() where T : TBaseType
        {
            _typeList.Remove(typeof(T));
        }

        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).GetTypeInfo().IsAssignableFrom(item))
            {
                throw new ArgumentException($"Given type ({item.AssemblyQualifiedName}) should be instance of {typeof(TBaseType).AssemblyQualifiedName} ", nameof(item));
            }
        } 
    }
}
