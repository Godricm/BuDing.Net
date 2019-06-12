using System;

namespace BuDing.Extensions
{
   public  static class EnumExtensions
    {
        public static T ConvertIntToEnum<T>(int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ConvertEnumToEnum<T>(object value)
        {
            return (T)Enum.ToObject(typeof(T), (int)value);
        }
    }
}
