namespace Spending.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Spending.Infrastructure.Data;
    using System.Threading.Tasks;

    public class MigrationMiddleware
    {
        private readonly RequestDelegate _next;

        public MigrationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationDbContext applicationDbContext)
        {
            await applicationDbContext.Database.MigrateAsync();
            await _next(context);
        }
    }
}
