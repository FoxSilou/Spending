namespace Spending.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Spending.Infrastructure.Data;
    using System.Threading.Tasks;

    public class SeedingMiddleware
    {
        private readonly RequestDelegate _next;

        public SeedingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationDbContextSeeding applicationDbContextSeeding)
        {
            await applicationDbContextSeeding.EnsureSeedData();
            await _next(context);
        }
    }
}
