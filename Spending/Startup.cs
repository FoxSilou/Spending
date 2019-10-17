using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spending.Domain.Contracts;
using Spending.Domain.Services;
using Spending.Infrastructure.Data;
using Spending.Infrastructure.Repositories;
using Spending.Middlewares;
using System;

namespace Spending
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<Func<ApplicationDbContext>>(s => () => new ApplicationDbContext(s.GetRequiredService<DbContextOptions<ApplicationDbContext>>()));

            // Add application services.
            services.AddScoped<ISpendingRepository, SpendingRepository>();
            services.AddScoped<ISpenderRepository, SpenderRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ISpendingService, SpendingService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles().UseStaticFiles();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
