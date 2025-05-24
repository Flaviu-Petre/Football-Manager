using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tema1.Core.DTOs;
using Tema1.Core.Interfaces;

namespace Tema1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/Teams?country=Romania&league=Liga1&page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams(
            [FromQuery] string? country = null,
            [FromQuery] string? league = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var teams = await _teamService.GetAllTeamsAsync();
            var teamList = teams.ToList();

            // Apply filtering
            if (!string.IsNullOrEmpty(country))
            {
                teamList = teamList.Where(t => t.Country.Contains(country, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(league))
            {
                teamList = teamList.Where(t => t.League.Contains(league, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply pagination
            var totalCount = teamList.Count;
            var pagedTeams = teamList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Add pagination info to response headers
            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            Response.Headers.Add("X-Page", page.ToString());
            Response.Headers.Add("X-Page-Size", pageSize.ToString());
            Response.Headers.Add("X-Total-Pages", ((int)Math.Ceiling((double)totalCount / pageSize)).ToString());

            return Ok(pagedTeams);
        }


        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        // GET: api/Teams/5/players
        [HttpGet("{id}/players")]
        public async Task<ActionResult<TeamWithPlayersDto>> GetTeamWithPlayers(int id)
        {

            var teamWithPlayers = await _teamService.GetTeamWithPlayersAsync(id);
            if (teamWithPlayers == null)
            {
                return NotFound();
            }
            return teamWithPlayers;
        }

        // POST: api/Teams
        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateTeam(TeamDto teamDto)
        {
            try
            {
                var result = await _teamService.CreateTeamAsync(teamDto);
                return CreatedAtAction(nameof(GetTeam), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Teams/sorted?sortBy=founded
        [HttpGet("sorted")]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamsSorted(
            [FromQuery] string sortBy = "name")
        {
            var teams = await _teamService.GetAllTeamsAsync();
            var teamList = teams.ToList();

            // Apply sorting
            var sortedTeams = sortBy.ToLower() switch
            {
                "name" => teamList.OrderBy(t => t.Name).ToList(),
                "country" => teamList.OrderBy(t => t.Country).ThenBy(t => t.Name).ToList(),
                "league" => teamList.OrderBy(t => t.League).ThenBy(t => t.Name).ToList(),
                "founded" => teamList.OrderBy(t => t.YearFounded).ToList(),
                "stadium" => teamList.OrderBy(t => t.Stadium).ToList(),
                _ => teamList.OrderBy(t => t.Name).ToList() // default to name
            };

            Console.WriteLine($"Sorted {sortedTeams.Count} teams by: {sortBy}");

            return Ok(sortedTeams);
        }

        // PATCH: api/Teams/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<TeamDto>> UpdateTeam(int id, TeamUpdateDto teamUpdateDto)
        {
            try
            {
                var result = await _teamService.UpdateTeamAsync(id, teamUpdateDto);
                if (result == null)
                {
                    return NotFound($"Team with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}