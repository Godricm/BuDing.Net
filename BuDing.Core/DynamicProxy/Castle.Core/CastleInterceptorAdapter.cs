using System.Reflection;
using System.Threading.Tasks;
using BuDing.Core.Threading;
using Castle.DynamicProxy;

namespace BuDing.DynamicProxy
{
    public class CastleInterceptorAdapter<TInterceptor>: Castle.DynamicProxy.IInterceptor
        where TInterceptor : IInterceptor
    {
        private static readonly MethodInfo MethodExecuteWithoutReturnValueAsync =
            typeof(CastleInterceptorAdapter<TInterceptor>).GetMethod(nameof(ExecuteWithoutReturnValueAsync),BindingFlags.NonPublic|BindingFlags.Instance);

        private static readonly MethodInfo MethodExecuteWithReturnValueAsync =
            typeof(CastleInterceptorAdapter<TInterceptor>).GetMethod(nameof(ExecuteWithReturnValueAsync),
                BindingFlags.NonPublic | BindingFlags.Instance);


       private readonly IInterceptor _abpInterceptor;

       public CastleInterceptorAdapter(IInterceptor abpInterceptor)
       {
           _abpInterceptor = abpInterceptor;
       }

       public void Intercept(IInvocation invocation)
        {
            var processdInfo = invocation.CaptureProceedInfo();
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            if (method.IsAsync())
            {
                //调用异步方法
                InterceptorAsyncMethod(invocation,processdInfo);
            }
            else
            {
                //调用同步方法
                InterceptSyncMethod(invocation,processdInfo);
            }
        }

        private void InterceptSyncMethod(IInvocation invocation, IInvocationProceedInfo proceedInfo)
        {
           _abpInterceptor.Intercept(new CastleMethodInvaocationAdapter(invocation,proceedInfo));
        }

        private void InterceptorAsyncMethod(IInvocation invocation, IInvocationProceedInfo proceedInfo)
        {
            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue =
                    MethodExecuteWithReturnValueAsync.Invoke(this, new object[] {invocation, proceedInfo});
            }
            else
            {
                invocation.ReturnValue = MethodExecuteWithReturnValueAsync
                    .MakeGenericMethod(invocation.Method.ReturnType.GenericTypeArguments[0])
                    .Invoke(this, new object[] {invocation, proceedInfo});
            }
        }

        private async Task ExecuteWithoutReturnValueAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo)
        {
            await Task.Yield();
            await _abpInterceptor.InterceptAsync(new CastleMethodInvaocationAdapter(invocation, proceedInfo));
        }

        private async Task<T> ExecuteWithReturnValueAsync<T>(IInvocation invocation,
            IInvocationProceedInfo proceedInfo)
        {
            await Task.Yield();
            await _abpInterceptor.InterceptAsync(new CastleMethodInvaocationAdapter(invocation, proceedInfo));
            return await (Task<T>) invocation.ReturnValue;
        }
    }
}
