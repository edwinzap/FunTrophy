using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : AdminControllerBase
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
        /// <param name="teamId">Team id</param>
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
        /// Get all the teams of the race
        /// </summary>
        /// <param name="colorId">Color Id</param>
        /// <returns>List of teams</returns>
        [AllowAnonymous]
        [HttpGet("ByColor/{colorId}")]
        [ProducesResponseType(typeof(List<TeamDto>), 200)]
        public async Task<IActionResult> GetTeamsByColor(int colorId)
        {
            var teams = await _teamService.GetByColor(colorId);
            return Ok(teams);
        }

        /// <summary>
        /// Get all the teams of the race
        /// </summary>
        /// <param name="raceId">Race id</param>
        /// <returns>List of teams</returns>
        [AllowAnonymous]
        [HttpGet("ByRace/{raceId}")]
        [ProducesResponseType(typeof(List<TeamDto>), 200)]
        public async Task<IActionResult> GetTeamsByRace(int raceId)
        {
            var teams = await _teamService.GetByRace(raceId);
            return Ok(teams);
        }
    }
}