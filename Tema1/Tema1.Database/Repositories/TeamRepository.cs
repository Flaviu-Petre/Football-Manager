using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tema1.Database.Repositories;
using Tema1.Database.Entities;
using Tema1.Database;
using Tema1.Database.Context;

namespace Tema1.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly FootballDbContext _context;

        public TeamRepository(FootballDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task<Team> GetTeamWithPlayersAsync(int id)
        {
            return await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Team> AddTeamAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<bool> TeamExistsAsync(int id)
        {
            return await _context.Teams.AnyAsync(t => t.Id == id);
        }
    }
}