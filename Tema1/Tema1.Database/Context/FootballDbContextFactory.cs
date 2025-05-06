using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Tema1.Database.Context;
using Tema1.Infrastructure.Config;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Tema1.Database.Context
{
    internal class FootballDbContextFactory : IDesignTimeDbContextFactory<FootballDbContext>
    {
        public FootballDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            AppConfig.Init(configuration);

            return new FootballDbContext();
        }
    }


}