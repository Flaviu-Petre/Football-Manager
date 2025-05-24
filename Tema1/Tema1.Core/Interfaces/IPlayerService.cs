using System.Collections.Generic;
using System.Threading.Tasks;
using Tema1.Core.DTOs;
using Tema1.Core.Services;

namespace Tema1.Core.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();
        Task<IEnumerable<PlayerDto>> GetPlayersByTeamIdAsync(int teamId);
        Task<PlayerDto> GetPlayerByIdAsync(int id);
        Task<PlayerDto> CreatePlayerAsync(PlayerDto playerDto);
        Task<PlayerDto?> UpdatePlayerAsync(int id, PlayerUpdateDto playerUpdateDto);
    }
}