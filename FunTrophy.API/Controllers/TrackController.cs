using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly ILogger<TrackController> _logger;
        private readonly ITrackService _trackService;

        public TrackController(ILogger<TrackController> logger, ITrackService trackService)
        {
            _logger = logger;
            _trackService = trackService;
        }

        /// <summary>
        /// Create a new track
        /// </summary>
        /// <param name="track">The track</param>
        /// <returns>The new track Id</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateTrack([FromBody] AddTrackDto track)
        {
            var trackId = await _trackService.Create(track);
            return Ok(trackId);
        }

        /// <summary>
        /// Remove the track with the given Id
        /// </summary>
        /// <param name="trackId">Track Id</param>
        /// <returns></returns>
        [HttpDelete("{trackId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveTrack(int trackId)
        {
            await _trackService.Remove(trackId);
            return Ok();
        }

        /// <summary>
        /// Update the track with the given Id
        /// </summary>
        /// <param name="trackId">Track Id</param>
        /// <param name="track">Track object</param>
        /// <returns></returns>
        [HttpPut("{trackId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTrack(int trackId, [FromBody] UpdateTrackDto track)
        {
            await _trackService.Update(trackId, track);
            return Ok();
        }

        /// <summary>
        /// Get a list of all tracks
        /// </summary>
        /// <param name="raceId">Race Id</param>
        /// <returns>List of tracks</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<TrackDto>), 200)]
        public async Task<IActionResult> GetAllTracks(int raceId)
        {
            var tracks = await _trackService.GetAll(raceId);
            return Ok(tracks);
        }
    }
}