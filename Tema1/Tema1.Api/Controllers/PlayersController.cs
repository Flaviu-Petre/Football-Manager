using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tema1.Core.DTOs;
using Tema1.Core.Interfaces;


namespace Tema1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetAllPlayers(
            [FromQuery] string? position = null,
            [FromQuery] string? nationality = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var players = await _playerService.GetAllPlayersAsync();

            var playerList = players.ToList();

            // Apply filtering
            if (!string.IsNullOrEmpty(position))
            {
                playerList = playerList.Where(p => p.Position.Contains(position, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(nationality))
            {
                playerList = playerList.Where(p => p.Nationality.Contains(nationality, StringComparison.OrdinalIgnoreCase)).ToList();      
            }

            // Apply pagination
            var totalCount = playerList.Count;

            var pagedPlayers = playerList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            // Add pagination info to response headers
            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            Response.Headers.Add("X-Page", page.ToString());
            Response.Headers.Add("X-Page-Size", pageSize.ToString());
            Response.Headers.Add("X-Total-Pages", ((int)Math.Ceiling((double)totalCount / pageSize)).ToString());

            return Ok(pagedPlayers);
        }
        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return player;
        }

        // GET: api/Players/team/5
        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersByTeam(int teamId)
        {
            var players = await _playerService.GetPlayersByTeamIdAsync(teamId);
            return Ok(players);
        }

        // POST: api/Players
        [HttpPost]
        public async Task<ActionResult<PlayerDto>> CreatePlayer(PlayerDto playerDto)
        {
            try
            {
                var result = await _playerService.CreatePlayerAsync(playerDto);
                return CreatedAtAction(nameof(GetPlayer), new { id = result.Id }, result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Players/sorted?sortBy=goals
        [HttpGet("sorted")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersSorted(
            [FromQuery] string sortBy = "name")
        {
            var players = await _playerService.GetAllPlayersAsync();
            var playerList = players.ToList();

            Console.WriteLine($"Received sortBy parameter: '{sortBy}'");

            // Apply sorting - handle both lowercase and property names
            var sortedPlayers = sortBy.ToLower() switch
            {
                "name" or "lastname" => playerList.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList(),
                "age" => playerList.OrderBy(p => p.Age).ToList(),
                "goals" or "goalsscored" => playerList.OrderByDescending(p => p.GoalsScored).ToList(),
                "appearances" => playerList.OrderByDescending(p => p.Appearances).ToList(),
                "shirtnumber" => playerList.OrderBy(p => p.ShirtNumber).ToList(),
                _ => playerList.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList() // default to name
            };

            Console.WriteLine($"Sorted {sortedPlayers.Count} players by: {sortBy}");

            return Ok(sortedPlayers);
        }
    }
}