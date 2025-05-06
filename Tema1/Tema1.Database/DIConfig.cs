using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tema1.Database.Context;
using Tema1.Database.Repositories;
using Tema1.Infrastructure.Repositories;

namespace Tema1.Database;

public static class DIConfig
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddDbContext<FootballDbContext>();
        services.AddScoped<DbContext, FootballDbContext>();

        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();



        return services;
    }
}