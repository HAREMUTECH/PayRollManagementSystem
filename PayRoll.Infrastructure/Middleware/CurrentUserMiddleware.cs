using Microsoft.AspNetCore.Http;
using PayRoll.Application.Interfaces;

namespace PayRoll.Infrastructure.Middleware
{
    public class CurrentUserMiddleware : IMiddleware
    {
        private readonly ICurrentUserInitializer _currentUserInitializer;

        public CurrentUserMiddleware(ICurrentUserInitializer currentUserInitializer) =>
            _currentUserInitializer = currentUserInitializer;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _currentUserInitializer.SetCurrentUser(context.User);

            await next(context);
        }
    }
}