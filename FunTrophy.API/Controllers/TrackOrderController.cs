using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackOrderController : ControllerBase
    {
        private readonly ILogger<TrackOrderController> _logger;
        private readonly ITrackOrderService _trackOrderService;

        public TrackOrderController(ILogger<TrackOrderController> logger, ITrackOrderService trackService)
        {
            _logger = logger;
            _trackOrderService = trackService;
        }

        /// <summary>
        /// Create a new trackOrder
        /// </summary>
        /// <param name="trackOrder">The trackOrder</param>
        /// <returns>The new trackOrder Id</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateTrackOrder([FromBody] AddTrackOrderDto trackOrder)
        {
            var trackOrderId = await _trackOrderService.Create(trackOrder);
            return Ok(trackOrderId);
        }

        /// <summary>
        /// Remove the trackOrder with the given Id
        /// </summary>
        /// <param name="trackOrderId">TrackOrder Id</param>
        /// <returns></returns>
        [HttpDelete("{trackOrderId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveTrackOrder(int trackOrderId)
        {
            await _trackOrderService.Remove(trackOrderId);
            return Ok();
        }

        /// <summary>
        /// Update the track order with the given Id
        /// </summary>
        /// <param name="trackOrderId">TrackOrder Id</param>
        /// <param name="sortOrder">Sort order</param>
        /// <returns></returns>
        [HttpPut("{trackOrderId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTrackOrder(int trackOrderId, int sortOrder)
        {
            await _trackOrderService.Update(trackOrderId, sortOrder);
            return Ok();
        }
    }
}