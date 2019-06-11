using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace BuDing.Core.Dependency.Attributes
{
   public  class DependencyAttribute:Attribute
    {
        public DependencyAttribute()
        {
        }

        public DependencyAttribute(ServiceLifetime? lifetime)
        {
            Lifetime = lifetime;
        }

        /// <summary>
        /// 生命周期
        /// </summary>
        public  virtual ServiceLifetime? Lifetime { get; set; }

        public  virtual bool TryRegister { get; set; }

        public  virtual  bool ReplaceServices { get; set; }
    }
}
