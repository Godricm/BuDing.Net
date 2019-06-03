using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    public static class FacilityHelper
    {
        public static bool DoesKernelNotAlreadyContainFacility<T>(IWindsorContainer container)
        {
            return (container.Kernel.GetFacilities().ToList().FirstOrDefault(x => x.GetType() == typeof(T)) == null);
        }
    }
}
