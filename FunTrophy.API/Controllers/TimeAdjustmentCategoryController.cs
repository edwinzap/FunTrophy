using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeAdjustmentCategoryController : ControllerBase
    {
        private readonly ILogger<TimeAdjustmentCategoryController> _logger;
        private readonly ITimeAdjustmentCategoryService _timeAdjustmentCategoryService;

        public TimeAdjustmentCategoryController(ILogger<TimeAdjustmentCategoryController> logger, ITimeAdjustmentCategoryService timeAdjustmentCategoryService)
        {
            _logger = logger;
            _timeAdjustmentCategoryService = timeAdjustmentCategoryService;
        }

        /// <summary>
        /// Create a new time adjustment category
        /// </summary>
        /// <param name="timeAdjustmentCategory">The time adjustment category</param>
        /// <returns>The new time adjustment category Id</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateTimeAdjustmentCategory([FromBody] AddTimeAdjustmentCategoryDto timeAdjustmentCategory)
        {
            var timeAdjustmentCategoryId = await _timeAdjustmentCategoryService.Create(timeAdjustmentCategory);
            return Ok(timeAdjustmentCategoryId);
        }

        /// <summary>
        /// Remove the time adjustment category with the given Id
        /// </summary>
        /// <param name="timeAdjustmentCategoryId">Time adjustment category Id</param>
        /// <returns></returns>
        [HttpDelete("{timeAdjustmentCategoryId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveTimeAdjustmentCategory(int timeAdjustmentCategoryId)
        {
            await _timeAdjustmentCategoryService.Remove(timeAdjustmentCategoryId);
            return Ok();
        }

        /// <summary>
        /// Update the time adjustment category with the given Id
        /// </summary>
        /// <param name="timeAdjustmentCategoryId">Time adjustment category Id</param>
        /// <param name="timeAdjustmentCategory">Time adjustment category object</param>
        /// <returns></returns>
        [HttpPut("{timeAdjustmentCategoryId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTimeAdjustmentCategory(int timeAdjustmentCategoryId, [FromBody] UpdateTimeAdjustmentCategoryDto timeAdjustmentCategory)
        {
            await _timeAdjustmentCategoryService.Update(timeAdjustmentCategoryId, timeAdjustmentCategory);
            return Ok();
        }

        /// <summary>
        /// Get a list of all time adjustment categories of a team
        /// </summary>
        /// <param name="raceId">Race Id</param>
        /// <returns>List of time adjustment categories of a team</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<TimeAdjustmentCategoryDto>), 200)]
        public async Task<IActionResult> GetAllTimeAdjustmentCategories(int raceId)
        {
            var timeAdjustmentCategorys = await _timeAdjustmentCategoryService.GetAll(raceId);
            return Ok(timeAdjustmentCategorys);
        }
    }
}