using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ResearchJournals.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var dataSettings = Path.Combine(Directory.GetCurrentDirectory(), "datasettings.json");

            if (!File.Exists(dataSettings))
                throw new FileNotFoundException(
                    "Please add the default connection string to the design-time settings file.", dataSettings);

            var config = new ConfigurationBuilder()
                .AddJsonFile(dataSettings).Build();

            var connectionString = config.GetConnectionString("ResearchJournalsDb");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
