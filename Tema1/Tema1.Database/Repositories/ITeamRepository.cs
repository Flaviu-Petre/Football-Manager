using System.Collections.Generic;
using System.Threading.Tasks;
using Tema1.Database.Entities;

namespace Tema1.Database.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(int id);
        Task<Team> GetTeamWithPlayersAsync(int id);
        Task<Team> AddTeamAsync(Team team);

        Task<bool> TeamExistsAsync(int id);
    }
}