using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing
{
    public class AppBuilderOptions
    {
        public readonly IServiceProvider ServiceProvider;

        public AppBuilderOptions(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
