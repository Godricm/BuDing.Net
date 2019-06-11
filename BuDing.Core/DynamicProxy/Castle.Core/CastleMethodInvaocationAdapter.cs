using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BuDing.Core.Threading;
using Castle.DynamicProxy;

namespace BuDing.DynamicProxy
{
    public class CastleMethodInvaocationAdapter : IMethodInvocation
    {
        protected IInvocation Invocation { get; }

        protected IInvocationProceedInfo ProceedInfo { get; }

        public object[] Arguments => Invocation.Arguments;
        public IReadOnlyDictionary<string, object> ArgumentsDictionary => _lazyArgumentsDictionary;

        private readonly Lazy<IReadOnlyDictionary<string, object>> _lazyArgumentsDictionary;

        public CastleMethodInvaocationAdapter(IInvocation invocation, IInvocationProceedInfo proceedInfo)
        {
            Invocation = invocation;
            ProceedInfo = proceedInfo;
            _lazyArgumentsDictionary = new Lazy<IReadOnlyDictionary<string, object>>(GetArgumentsDictionary);
        }

        public Type[] GenericArguments => Invocation.GenericArguments;
        public object TargetObject => Invocation.InvocationTarget ?? Invocation.MethodInvocationTarget;
        public MethodInfo Method => Invocation.MethodInvocationTarget ?? Invocation.Method;
        public object ReturnValue
        {
            get => _actualReturnValue ?? Invocation.ReturnValue;
            set => Invocation.ReturnValue = value;
        }
        private object _actualReturnValue;
        public void Processed()
        {
            ProceedInfo.Invoke();
            if (Invocation.Method.IsAsync())
            {
                AsyncHelper.RunSync(() => (Task)Invocation.ReturnValue);
            }
        }

        public Task ProcessedAsync()
        {
            ProceedInfo.Invoke();
            _actualReturnValue = Invocation.ReturnValue;

            return Invocation.Method.IsAsync()
                ? (Task)
                _actualReturnValue
                : Task.FromResult(_actualReturnValue);
        }

        private IReadOnlyDictionary<string, object> GetArgumentsDictionary()
        {
            var dict = new Dictionary<string, object>();
            var methodParameters = Method.GetParameters();
            for (int i = 0; i < methodParameters.Length; i++)
            {
                dict[methodParameters[i].Name] = Invocation.Arguments[i];
            }

            return dict;
        }
    }
}
