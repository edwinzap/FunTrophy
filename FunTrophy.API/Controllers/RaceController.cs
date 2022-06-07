using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly ILogger<RaceController> _logger;
        private readonly IRaceService _raceService;

        public RaceController(ILogger<RaceController> logger, IRaceService raceService)
        {
            _logger = logger;
            _raceService = raceService;
        }

        /// <summary>
        /// Get the race with the given Id
        /// </summary>
        /// <param name="raceId">Race Id</param>
        /// <returns>The race</returns>
        [HttpGet("{raceId}")]
        [ProducesResponseType(typeof(RaceDto), 200)]
        public async Task<IActionResult> GetRace(int raceId)
        {
            var race = await _raceService.Get(raceId);
            return Ok(race);
        }

        /// <summary>
        /// Create a new race
        /// </summary>
        /// <param name="race">The race</param>
        /// <returns>The new race Id</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateRace([FromBody] AddOrUpdateRaceDto race)
        {
            var raceId = await _raceService.Create(race);
            return Ok(raceId);
        }

        /// <summary>
        /// Remove the race with the given Id
        /// </summary>
        /// <param name="raceId">Race Id</param>
        /// <returns></returns>
        [HttpDelete("{raceId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveRace(int raceId)
        {
            await _raceService.Remove(raceId);
            return Ok();
        }

        /// <summary>
        /// Update the race with the given Id
        /// </summary>
        /// <param name="raceId">Race Id</param>
        /// <param name="race">Race object</param>
        /// <returns></returns>
        [HttpPut("{raceId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateRace(int raceId, [FromBody] AddOrUpdateRaceDto race)
        {
            await _raceService.Update(raceId, race);
            return Ok();
        }

        [HttpPut("{raceId}/end/{isEnded}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> EndRace(int raceId, bool isEnded)
        {
            await _raceService.End(raceId, isEnded);
            return Ok();
        }


        /// <summary>
        /// Get a list of all races
        /// </summary>
        /// <returns>List of races</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<RaceDto>), 200)]
        public async Task<IActionResult> GetAllRaces()
        {
            var races = await _raceService.GetAll();
            return Ok(races);
        }

        /// <summary>
        /// Reset the race
        /// </summary>
        /// <param name="raceId">Race Id</param>
        /// <returns>The new race Id</returns>
        [HttpPost("reset/{raceId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ResetRace(int raceId)
        {
            await _raceService.Reset(raceId);
            return Ok();
        }
    }
}