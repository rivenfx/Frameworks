﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.Uow;

namespace Riven.AspNetCore.Mvc.Uow
{
    public class AspNetCoreUowMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var serviceProvider = context.RequestServices;

            var endpoint = context.GetEndpoint();
            var unitOfWorkAttribute = endpoint.Metadata.GetMetadata<UnitOfWorkAttribute>();

            // 为空的话获取默认的
            if (unitOfWorkAttribute == null)
            {
                unitOfWorkAttribute = serviceProvider.GetRequiredService<UnitOfWorkAttribute>();
            }

            // 如果是禁用
            if (unitOfWorkAttribute.IsDisabled)
            {
                await next(context);
                return;
            }

            // 创建选项
            var unitOfWorkOptions = unitOfWorkAttribute.CreateOptions();

            // 启动工作单元
            var unitOfWorkManager = serviceProvider.GetRequiredService<IUnitOfWorkManager>();
            using (var uow = unitOfWorkManager.Begin(unitOfWorkOptions))
            {
                await next(context);
                await uow.CompleteAsync(context.RequestAborted);
            }
        }
    }
}