using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace BuDing.Core.Threading
{
   public  static class AsyncHelper
    {
        public static bool IsAsync(this MethodInfo method)
        {
            return (method.ReturnType == typeof(Task) || (method.ReturnType.GetTypeInfo().IsGenericType &&
                                                          method.ReturnType.GetGenericTypeDefinition() ==
                                                          typeof(Task<>)));
        }

        public static TResult RunAsync<TResult>(Func<Task<TResult>> func)
        {
            return AsyncContext.Run(func);
        }

        public static void RunSync(Func<Task> action)
        {
            AsyncContext.Run(action);
        }
    }
}
