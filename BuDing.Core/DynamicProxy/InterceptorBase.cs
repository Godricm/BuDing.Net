using System.Threading.Tasks;

namespace BuDing.DynamicProxy
{
    public abstract class InterceptorBase:IInterceptor
    {
        public abstract void Intercept(IMethodInvocation invocation);

        public virtual Task InterceptAsync(IMethodInvocation invocation)
        {
            Intercept(invocation);
            return Task.CompletedTask;
        }
    }
}
