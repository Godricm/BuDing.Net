using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Dependency
{
    /// <summary>
    /// 服务注册委托事件
    /// </summary>
    public class ServiceRegistrationActionList:List<Action<IOnServiceRegistredContext>>
    {
    }
}
