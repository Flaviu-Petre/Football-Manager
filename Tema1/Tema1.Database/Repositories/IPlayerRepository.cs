using System.Collections.Generic;
using System.Threading.Tasks;
using Tema1.Database.Entities;


namespace Tema1.Database.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<IEnumerable<Player>> GetPlayersByTeamIdAsync(int teamId);
        Task<Player> GetPlayerByIdAsync(int id);
        Task<Player> AddPlayerAsync(Player player);
        Task<Player?> UpdatePlayerAsync(Player player);
    }
}