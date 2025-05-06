using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tema1.Core.DTOs;
using Tema1.Core.Interfaces;
using Tema1.Database.Entities;
using Tema1.Database.Repositories;


namespace Tema1.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllTeamsAsync();
            return teams.Select(MapTeamToDto);
        }

        public async Task<TeamDto> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetTeamByIdAsync(id);
            return team != null ? MapTeamToDto(team) : null;
        }

        public async Task<TeamWithPlayersDto> GetTeamWithPlayersAsync(int id)
        {
            var team = await _teamRepository.GetTeamWithPlayersAsync(id);
            if (team == null) 
                return null;

            return new TeamWithPlayersDto
            {
                Id = team.Id,
                Name = team.Name,
                Country = team.Country,
                League = team.League,
                YearFounded = team.YearFounded,
                Stadium = team.Stadium,
                CoachName = team.CoachName,
                Players = team.Players?.Select(p => new PlayerDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth,
                    Nationality = p.Nationality,
                    Position = p.Position,
                    ShirtNumber = p.ShirtNumber,
                    GoalsScored = p.GoalsScored,
                    Appearances = p.Appearances,
                    TeamId = p.TeamId,
                    TeamName = team.Name
                }).ToList()
            };
        }

        public async Task<TeamDto> CreateTeamAsync(TeamDto teamDto)
        {
            try
            {
                var team = new Team
                {
                    Name = teamDto.Name ?? throw new ArgumentNullException(nameof(teamDto.Name)),
                    Country = teamDto.Country,
                    League = teamDto.League,
                    YearFounded = teamDto.YearFounded,
                    Stadium = teamDto.Stadium,
                    CoachName = teamDto.CoachName
                };

                var result = await _teamRepository.AddTeamAsync(team);
                teamDto.Id = result.Id;
                return teamDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating team: {ex.Message}");
                throw; 
            }
        }

        // Helper method for mapping
        private static TeamDto MapTeamToDto(Team team)
        {
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Country = team.Country,
                League = team.League,
                YearFounded = team.YearFounded,
                Stadium = team.Stadium,
                CoachName = team.CoachName
            };
        }
    }
}