using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Core.Dependency.Attributes
{
    /// <summary>
    /// 禁用依赖注入标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DisableConventionalsRegistratorAttribute:Attribute
    {
    }
}
