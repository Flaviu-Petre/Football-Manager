using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Tema1.Database.Entities;
using Tema1.Infrastructure.Config;



namespace Tema1.Database.Context
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext() { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && AppConfig.ConnectionStrings?.FootballDatabase is not null)
            {
                optionsBuilder.UseSqlServer(AppConfig.ConnectionStrings.FootballDatabase);

                if (AppConfig.ConsoleLogQueries)
                {
                    optionsBuilder.LogTo(Console.WriteLine);
                }
            }
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}