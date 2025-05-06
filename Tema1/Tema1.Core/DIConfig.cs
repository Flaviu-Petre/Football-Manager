using Microsoft.Extensions.DependencyInjection;
using Tema1.Core.Services;
using Tema1.Database.Repositories;
using Tema1.Infrastructure.Repositories;
using Tema1.Core.Interfaces; 

namespace Tema1.Core;

public static class DIConfig
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<Tema1.Core.Interfaces.ITeamService, Tema1.Core.Services.TeamService>();
        services.AddScoped<Tema1.Core.Interfaces.IPlayerService, Tema1.Core.Services.PlayerService>();

        return services;
    }
}