using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.AspNetCore.Mvc.Uow;
using Riven.Uow;
using Microsoft.AspNetCore.Builder;

namespace Riven
{
    public static class RivenAspNetCoreUowExtensions
    {
        /// <summary>
        /// 添加 AspNet Core UnitOfWork
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCoreUow(this IServiceCollection services, Action<UnitOfWorkAttribute> optionsAction = null)
        {
            services.AddOptions<UnitOfWorkAttribute>();
            if (optionsAction != null)
            {
                services.Configure(optionsAction);
            }

            services.TryAddTransient<AspNetCoreUowMiddleware>();

            return services;
        }

        /// <summary>
        /// 添加 Riven AspNet Core Uow 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRivenAspnetCoreUow(this IApplicationBuilder app)
        {
            app.UseMiddleware<AspNetCoreUowMiddleware>();
            return app;
        }
    }
}
