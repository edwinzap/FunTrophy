using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColorController : ControllerBase
    {
        private readonly ILogger<ColorController> _logger;
        private readonly IColorService _colorService;

        public ColorController(ILogger<ColorController> logger, IColorService colorService)
        {
            _logger = logger;
            _colorService = colorService;
        }

        /// <summary>
        /// Create a new color
        /// </summary>
        /// <param name="color">The color</param>
        /// <returns>The new color Id</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateColor([FromBody] AddColorDto color)
        {
            var colorId = await _colorService.Create(color);
            return Ok(colorId);
        }

        /// <summary>
        /// Remove the color with the given Id
        /// </summary>
        /// <param name="colorId">Color Id</param>
        /// <returns></returns>
        [HttpDelete("{colorId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveColor(int colorId)
        {
            await _colorService.Remove(colorId);
            return Ok();
        }

        /// <summary>
        /// Update the color with the given Id
        /// </summary>
        /// <param name="colorId">Color Id</param>
        /// <param name="color">Color object</param>
        /// <returns></returns>
        [HttpPut("{colorId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateColor(int colorId, [FromBody] UpdateColorDto color)
        {
            await _colorService.Update(colorId, color);
            return Ok();
        }

        /// <summary>
        /// Get a list of all colors
        /// </summary>
        /// <param name="raceId">Race Id</param>
        /// <returns>List of colors</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<TeamDto>), 200)]
        public async Task<IActionResult> GetAllColors(int raceId)
        {
            var colors = await _colorService.GetAll(raceId);
            return Ok(colors);
        }
    }
}