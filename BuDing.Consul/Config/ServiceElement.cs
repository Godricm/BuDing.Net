using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuDing.Consul.Config
{
    public class ServiceElement
    {
        public  string Name { get; set; }

        public  string Description { get; set; }

        /// <summary>
        /// 服务标签参数，可用于服务分级，可多项，逗号分割
        /// </summary>
        public  string ServiceTags { get; set; }
    }
}
