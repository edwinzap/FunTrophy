using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultController: ControllerBase
    {
        private readonly ILogger<ResultController> _logger;
        private readonly IResultService _resultService;

        public ResultController(ILogger<ResultController> logger, IResultService resultService)
        {
            _logger = logger;
            _resultService = resultService;
        }

        /// <summary>
        /// Get the results of each team for the track
        /// </summary>
        /// <param name="trackId">Track id</param>
        /// <returns>The results</returns>
        [HttpGet("ByTrack/{trackId}")]
        [ProducesResponseType(typeof(List<TrackResultDto>), 200)]
        public async Task<IActionResult> GetTrackResults(int trackId)
        {
            var results = await _resultService.GetTrackResults(trackId);
            return Ok(results);
        }

        /// <summary>
        /// Get the results of each track for the team
        /// </summary>
        /// <param name="teamId">Team id</param>
        /// <returns>The results</returns>
        [HttpGet("ByTeam/{teamId}")]
        [ProducesResponseType(typeof(List<TeamResultDto>), 200)]
        public async Task<IActionResult> GetTeamResults(int teamId)
        {
            var results = await _resultService.GetTeamResults(teamId);
            return Ok(results);
        }
    }
}
