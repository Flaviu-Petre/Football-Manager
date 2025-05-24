using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tema1.Database.Repositories;
using Tema1.Database.Entities;
using Tema1.Database.Context;

namespace Tema1.Database.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly FootballDbContext _context;

        public PlayerRepository(FootballDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetPlayersByTeamIdAsync(int teamId)
        {
            return await _context.Players
                .Where(p => p.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            return await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Player> AddPlayerAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _context.Players
                .Include(p => p.Team)
                .ToListAsync();
        }

        public async Task<Player?> UpdatePlayerAsync(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
            return player;
        }
    }
}