using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuDing.Consul.Config
{
    public class ConsulOptions
    {

        /// <summary>
        /// 心跳周期参数（s）
        /// </summary>
        public  int HeartInterval{ get; set; }

        /// <summary>
        /// 刷新周期参数（s），刷新服务状态、键值事件
        /// </summary>
        public int RefreshBreakInterval { get; set; }


        /// <summary>
        /// 服务名称参数（同服务多负载相同，不同服务不可相同）
        /// </summary>
        public  string ServiceName { get; set; }

        /// <summary>
        /// 服务标签参数，可用于服务分级，可多项，用，分割
        /// </summary>
        public string ServiceTags { get; set; }

        /// <summary>
        /// 服务端口
        /// </summary>
        public string ServicePort { get; set; }

        /// <summary>
        /// 心跳检查http接口，返回200状态即可，被动检查接口，不提供被动检查接口将按心跳周期主动向consul提交心跳请求
        /// </summary>
        public string HttpCheck { get; set; }

        /// <summary>
        /// 心跳检查Tcp接口，连接成功即可，被动检查接口，不提供被动检查接口，将按心跳周期主动向Consul提交请求
        /// </summary>
        public string TcpCheck { get; set; }

        /// <summary>
        /// 可发现服务
        /// </summary>
        public ServiceElement[] Services { get; set; }

        /// <summary>
        /// 需监视分布式缓存键值
        /// </summary>
        public KeyValueElement[] KeyValues { get; set; }
    }
}
