using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackTimeController : AuthenticatedControllerBase
    {
        private readonly ILogger<TrackTimeController> _logger;
        private readonly ITrackTimeService _trackTimeService;

        public TrackTimeController(ILogger<TrackTimeController> logger, ITrackTimeService trackTimeService)
        {
            _logger = logger;
            _trackTimeService = trackTimeService;
        }

        /// <summary>
        /// Save the lap for the team
        /// </summary>
        /// <param name="teamId">Team id</param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SaveTeamLap(int teamId)
        {
            await _trackTimeService.SaveTeamLap(teamId);
            return Ok();
        }

        /// <summary>
        /// Get all lap infos of color
        /// </summary>
        /// <param name="colorId">Color id</param>
        /// <returns>List of lap infos</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<TeamLapInfoDto>), 200)]
        public async Task<IActionResult> GetAllTeamLapInfos(int colorId)
        {
            var laps = await _trackTimeService.GetTeamLaps(colorId);
            return Ok(laps);
        }
    }
}