using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;

namespace BuDing.AutoFac
{
    public static class ServiceCollectionExtensions
    {
        //public static IServiceProvider RegisterAssemblyByBasicInterface(this IServiceProvider services, Assembly assembly)
        //{
        //    var types = assembly.GetTypes()
        //        .Where(type => type != null && type.IsClass && !type.IsAbstract && !type.IsInterface);
        //    var containerBuilder = new ContainerBuilder();
        //    foreach (var type in types)
        //    {
        //        //是否有禁用依赖
        //        //属性依赖 设置标识
        //        //获取生命周期 1、Attribute 2、Interface Attribute>Interface

        //    } 

        //    ser
        //}
    }
}
