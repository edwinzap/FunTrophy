using FunTrophy.API.Authentication;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using FunTrophy.Shared.Model.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackTimeController : UserControllerBase
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
        [AuthorizeRoles(UserRoles.Admin, UserRoles.User)]
        [HttpPost("")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SaveTeamLap(int teamId)
        {
            if (int.TryParse(HttpContext.User.FindFirstValue("userId"), out int userId))
            {
                await _trackTimeService.SaveTeamLap(userId, teamId);
            }
            return Ok();
        }

        /// <summary>
        /// Undo the last change
        /// </summary>
        [AuthorizeRoles(UserRoles.Admin, UserRoles.User)]
        [HttpPost("undo")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Undo()
        {
            if (int.TryParse(HttpContext.User.FindFirstValue("userId"), out int userId))
            {
                await _trackTimeService.Undo(userId);
            }
            return Ok();
        }



        /// <summary>
        /// Get all lap infos of color
        /// </summary>
        /// <param name="colorId">Color id</param>
        /// <returns>List of lap infos</returns>
        [AuthorizeRoles(UserRoles.Admin, UserRoles.User)]
        [HttpGet("")]
        [ProducesResponseType(typeof(List<TeamLapInfoDto>), 200)]
        public async Task<IActionResult> GetAllTeamLapInfos(int colorId)
        {
            var laps = await _trackTimeService.GetTeamLaps(colorId);
            return Ok(laps);
        }
    }
}