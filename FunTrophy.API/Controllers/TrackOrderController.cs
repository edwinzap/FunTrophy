using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackOrderController : AdminControllerBase
    {
        private readonly ILogger<TrackOrderController> _logger;
        private readonly ITrackOrderService _trackOrderService;

        public TrackOrderController(ILogger<TrackOrderController> logger, ITrackOrderService trackService)
        {
            _logger = logger;
            _trackOrderService = trackService;
        }

        /// <summary>
        /// Get the track orders for the color
        /// </summary>
        /// <param name="colorId">Color id</param>
        /// <returns>List of track orders</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<TrackOrderDto>), 200)]
        public async Task<IActionResult> GetTrackOrders(int colorId)
        {
            var result = await _trackOrderService.GetAll(colorId);
            return Ok(result);
        }

        /// <summary>
        /// Update the track order with the given Id
        /// </summary>
        /// <param name="tracksOrder">Tracks order</param>
        /// <returns></returns>
        [HttpPut("")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTracksOrder([FromBody] UpdateTracksOrderDto tracksOrder)
        {
            await _trackOrderService.Update(tracksOrder);
            return Ok();
        }
    }
}