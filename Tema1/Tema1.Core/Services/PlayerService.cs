using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tema1.Core.DTOs;
using Tema1.Core.Exceptions;
using Tema1.Core.Interfaces;
using Tema1.Database.Entities;
using Tema1.Database.Repositories;


namespace Tema1.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public PlayerService(
            IPlayerRepository playerRepository,
            ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayersByTeamIdAsync(int teamId)
        {
            var players = await _playerRepository.GetPlayersByTeamIdAsync(teamId);
            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            string teamName = team?.Name ?? "Unknown Team";

            return players.Select(p => MapPlayerToDto(p, teamName));
        }

        public async Task<PlayerDto> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(id);
            if (player == null) 
                throw new NotFoundException("Player", id);

            string teamName = player.Team?.Name ?? "Unknown Team";
            return MapPlayerToDto(player, teamName);
        }

        public async Task<PlayerDto> CreatePlayerAsync(PlayerDto playerDto)
        {
            var team = await _teamRepository.GetTeamByIdAsync(playerDto.TeamId);
            if (team == null)
                throw new NotFoundException($"Team with ID {playerDto.TeamId} not found.");

            var player = new Player
            {
                FirstName = playerDto.FirstName,
                LastName = playerDto.LastName,
                DateOfBirth = playerDto.DateOfBirth,
                Nationality = playerDto.Nationality,
                Position = playerDto.Position,
                ShirtNumber = playerDto.ShirtNumber,
                GoalsScored = playerDto.GoalsScored,
                Appearances = playerDto.Appearances,
                TeamId = playerDto.TeamId
            };

            var result = await _playerRepository.AddPlayerAsync(player);
            playerDto.Id = result.Id;
            playerDto.TeamName = team.Name;
            return playerDto;
        }

        // Helper method for mapping
        private static PlayerDto MapPlayerToDto(Player player, string teamName)
        {
            return new PlayerDto
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                DateOfBirth = player.DateOfBirth,
                Nationality = player.Nationality,
                Position = player.Position,
                ShirtNumber = player.ShirtNumber,
                GoalsScored = player.GoalsScored,
                Appearances = player.Appearances,
                TeamId = player.TeamId,
                TeamName = teamName
            };
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
        {
            var players = await _playerRepository.GetAllPlayersAsync();

            // Create a dictionary of team names for efficient lookup
            var teamIds = players.Select(p => p.TeamId).Distinct().ToList();
            var teams = await Task.WhenAll(teamIds.Select(id => _teamRepository.GetTeamByIdAsync(id)));
            var teamNames = teams.Where(t => t != null).ToDictionary(t => t.Id, t => t.Name);

            return players.Select(p => MapPlayerToDto(p, teamNames.GetValueOrDefault(p.TeamId, "Unknown Team")));
        }

        public async Task<PlayerDto?> UpdatePlayerAsync(int id, PlayerUpdateDto playerUpdateDto)
        {
            var existingPlayer = await _playerRepository.GetPlayerByIdAsync(id);
            if (existingPlayer == null)
                throw new NotFoundException("Player", id);

            if (playerUpdateDto.TeamId.HasValue)
            {
                var team = await _teamRepository.GetTeamByIdAsync(playerUpdateDto.TeamId.Value);
                if (team == null)
                    throw new NotFoundException("Team", playerUpdateDto.TeamId.Value);
            }

            if (!string.IsNullOrWhiteSpace(playerUpdateDto.FirstName))
                existingPlayer.FirstName = playerUpdateDto.FirstName;

            if (!string.IsNullOrWhiteSpace(playerUpdateDto.LastName))
                existingPlayer.LastName = playerUpdateDto.LastName;

            if (playerUpdateDto.DateOfBirth.HasValue)
                existingPlayer.DateOfBirth = playerUpdateDto.DateOfBirth.Value;

            if (!string.IsNullOrWhiteSpace(playerUpdateDto.Nationality))
                existingPlayer.Nationality = playerUpdateDto.Nationality;

            if (!string.IsNullOrWhiteSpace(playerUpdateDto.Position))
                existingPlayer.Position = playerUpdateDto.Position;

            if (playerUpdateDto.ShirtNumber.HasValue)
                existingPlayer.ShirtNumber = playerUpdateDto.ShirtNumber.Value;

            if (playerUpdateDto.GoalsScored.HasValue)
                existingPlayer.GoalsScored = playerUpdateDto.GoalsScored.Value;

            if (playerUpdateDto.Appearances.HasValue)
                existingPlayer.Appearances = playerUpdateDto.Appearances.Value;

            if (playerUpdateDto.TeamId.HasValue)
                existingPlayer.TeamId = playerUpdateDto.TeamId.Value;

            var updatedPlayer = await _playerRepository.UpdatePlayerAsync(existingPlayer);
            if (updatedPlayer == null)
                return null;

            var teamEntity = await _teamRepository.GetTeamByIdAsync(updatedPlayer.TeamId);
            string teamName = teamEntity?.Name ?? "Unknown Team";

            return MapPlayerToDto(updatedPlayer, teamName);
        }
    }
}