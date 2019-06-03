using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Core.Reflection
{
   public  static class CreateInstanceHelper
    {
        public static T Resolve<T>(params object[] parameters) where T : class
        {
            return (T) Activator.CreateInstance(typeof(T), parameters);
        }
    }
}
