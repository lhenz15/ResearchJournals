using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResearchJournals.Domain.Models;
using ResearchJournals.Infrastructure.Data;

namespace ResearchJournals.Infrastructure
{
    public static class Setup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ResearchJournalsDb");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString,
                    b =>
                    {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    }));

            services.AddDefaultIdentity<Researcher>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
