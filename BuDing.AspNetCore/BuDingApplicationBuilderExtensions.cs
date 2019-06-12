using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace BuDing.AspNetCore
{
    public static class BuDingApplicationBuilderExtensions
    {
        public static void UseBuDing(this IApplicationBuilder app, Action<AppBuilderOptions> options = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            var builder=new AppBuilderOptions(app.ApplicationServices);
            options?.Invoke(builder);
        }
    }
}
