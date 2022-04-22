using FunTrophy.API.Services.Contracts;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        private readonly ITeamService _teamService;

        public TeamController(ILogger<TeamController> logger, ITeamService teamService)
        {
            _logger = logger;
            _teamService = teamService;
        }

        /// <summary>
        /// Create a new team
        /// </summary>
        /// <param name="team">The team</param>
        /// <returns>The new team Id</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateTeam([FromBody] AddTeamDto team)
        {
            var teamId = await _teamService.Create(team);
            return Ok(teamId);
        }

        /// <summary>
        /// Remove the team with the given Id
        /// </summary>
        /// <param name="teamId">Team Id</param>
        /// <returns></returns>
        [HttpDelete("{teamId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveTeam(int teamId)
        {
            await _teamService.Remove(teamId);
            return Ok();
        }

        /// <summary>
        /// Update the team with the given Id
        /// </summary>
        /// <param name="teamId">Team Id</param>
        /// <param name="team">Team object</param>
        /// <returns></returns>
        [HttpPut("{teamId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTeam(int teamId, [FromBody] UpdateTeamDto team)
        {
            await _teamService.Update(teamId, team);
            return Ok();
        }

        /// <summary>
        /// Get a list of all teams
        /// </summary>
        /// <returns>List of teams</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<TeamDto>), 200)]
        public async Task<IActionResult> GetAllTeams(int raceId)
        {
            var teams = await _teamService.GetAll(raceId);
            return Ok(teams);
        }
    }
}