using System.Collections.Generic;
using System.Threading.Tasks;
using Tema1.Core.DTOs;
using Tema1.Core.Services;


namespace Tema1.Core.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();

        Task<TeamDto> GetTeamByIdAsync(int id);

        Task<TeamWithPlayersDto> GetTeamWithPlayersAsync(int id);

        Task<TeamDto> CreateTeamAsync(TeamDto teamDto);
    }
}