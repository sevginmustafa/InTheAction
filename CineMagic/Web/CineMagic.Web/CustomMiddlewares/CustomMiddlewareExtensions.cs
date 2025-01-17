﻿namespace CineMagic.Web.CustomMiddlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCreateAdminCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CreateAdminCustomMiddleware>();
        }

        public static IApplicationBuilder UseCreatePrivacyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CreatePrivacyCustomMiddleware>();
        }
    }
}
