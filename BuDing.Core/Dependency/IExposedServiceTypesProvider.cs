using System;

namespace BuDing.Dependency
{
   public  interface IExposedServiceTypesProvider
   {
       Type[] GetExposedServiceTypes(Type targetType);
   }
}
