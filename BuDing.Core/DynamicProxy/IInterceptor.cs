using System.Threading.Tasks;

namespace BuDing.DynamicProxy
{
    public interface IInterceptor
    {
        void Intercept(IMethodInvocation invocation);

        Task InterceptAsync(IMethodInvocation invocation);
    }
}
