using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeAdjustmentController : AdminControllerBase
    {
        private readonly ILogger<TimeAdjustmentController> _logger;
        private readonly ITimeAdjustmentService _timeAdjustmentService;

        public TimeAdjustmentController(ILogger<TimeAdjustmentController> logger, ITimeAdjustmentService timeAdjustmentService)
        {
            _logger = logger;
            _timeAdjustmentService = timeAdjustmentService;
        }

        /// <summary>
        /// Create a new time adjustment
        /// </summary>
        /// <param name="timeAdjustment">The time adjustment</param>
        /// <returns>The new time adjustment Id</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateTimeAdjustment([FromBody] AddTimeAdjustmentDto timeAdjustment)
        {
            var timeAdjustmentId = await _timeAdjustmentService.Create(timeAdjustment);
            return Ok(timeAdjustmentId);
        }

        /// <summary>
        /// Remove the time adjustment with the given Id
        /// </summary>
        /// <param name="timeAdjustmentId">Time adjustment Id</param>
        /// <returns></returns>
        [HttpDelete("{timeAdjustmentId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveTimeAdjustment(int timeAdjustmentId)
        {
            await _timeAdjustmentService.Remove(timeAdjustmentId);
            return Ok();
        }

        /// <summary>
        /// Get a list of all time adjustments of a team
        /// </summary>
        /// <param name="teamId">Team Id</param>
        /// <returns>List of time adjustments of a team</returns>
        [AllowAnonymous]
        [HttpGet("byteam/{teamId}")]
        [ProducesResponseType(typeof(List<TimeAdjustmentDto>), 200)]
        public async Task<IActionResult> GetTimeAdjustmentsByTeam(int teamId)
        {
            var timeAdjustments = await _timeAdjustmentService.GetAllOfTeam(teamId);
            return Ok(timeAdjustments);
        }

        /// <summary>
        /// Get a list of all time adjustments of a category
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <returns>List of team time adjustments</returns>
        [AllowAnonymous]
        [HttpGet("bycategory/{categoryId}")]
        [ProducesResponseType(typeof(List<TeamTimeAdjustmentDto>), 200)]
        public async Task<IActionResult> GetTimeAdjustmentsByCategory(int categoryId)
        {
            var timeAdjustments = await _timeAdjustmentService.GetAllOfCategory(categoryId);
            return Ok(timeAdjustments);
        }

        /// <summary>
        /// Update the time adjustment
        /// </summary>
        /// <param name="timeAdjustement">Time adjustment</param>
        /// <returns></returns>
        [HttpPut("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> UpdateTimeAdjustment([FromBody] AddTimeAdjustmentDto timeAdjustement)
        {
            var timeAdjustmentId = await _timeAdjustmentService.Update(timeAdjustement);
            return Ok(timeAdjustmentId);
        }
    }
}