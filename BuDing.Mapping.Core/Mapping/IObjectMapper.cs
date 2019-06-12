using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Mapping
{
    public interface IObjectMapper
    {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
