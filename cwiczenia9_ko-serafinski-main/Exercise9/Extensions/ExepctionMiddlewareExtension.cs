﻿using Microsoft.AspNetCore.Diagnostics;

namespace Exercise8.Extensions;

public static class ExepctionMiddlewareExternsion
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder appBuilder)
    {
        appBuilder.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await File.AppendAllTextAsync("./logs.txt", contextFeature.Error.ToString() + "\n");
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Error");
                }
            });
        });
    }
}