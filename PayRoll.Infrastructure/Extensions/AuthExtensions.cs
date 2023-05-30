using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayRoll.Application.Interfaces;
using PayRoll.Infrastructure.Implementation;
using PayRoll.Infrastructure.Middleware;

namespace PayRoll.Infrastructure.Extensions
{
    internal static class AuthExtensions
    {
        internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddCurrentUser()
                // Must add identity before adding auth!
                //.AddIdentity()
                ;

            return services.AddJwtAuth(config);
        }

        internal static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app)
        {
            app.UseMiddleware<CurrentUserMiddleware>();

            return app;
        }

        private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
            services
                .AddScoped<CurrentUserMiddleware>()
                .AddScoped<ICurrentUser, CurrentUser>()
                .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());


    }
}