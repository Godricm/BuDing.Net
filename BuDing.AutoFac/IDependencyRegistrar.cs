using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.AutoFac
{
    /// <summary>
    /// 依赖注入接口
    /// </summary>
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder);
    }
}
