namespace Spending
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Spending.Infrastructure.Data;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var env = services.GetService<IHostingEnvironment>();
                var configuration = services.GetService<IConfiguration>();
                
                var dbontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbontext.Database.MigrateAsync();
                await dbontext.EnsureSeedData();
            }

            await host.RunAsync();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
