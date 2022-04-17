using FunTrophy.API.Mappers;
using FunTrophy.API.Services.Contracts;
using FunTrophy.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FunTrophy.API.Services
{
    public class TrackService : ServiceBase, ITrackService
    {
        private readonly ITrackMapper _mapper;

        public TrackService(FunTrophyContext dbContext, ITrackMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<int> Create(AddTrackDto track)
        {
            var dbTrack = _mapper.Map(track);
            _dbContext.Tracks.Add(dbTrack);
            await _dbContext.SaveChangesAsync();

            return dbTrack.Id;
        }

        public Task<List<TrackDto>> GetAll()
        {
            var task = _dbContext.Tracks.Select(x => _mapper.Map(x)).ToListAsync();
            return task;
        }

        public async Task Remove(int trackId)
        {
            var race = await _dbContext.Tracks.FindAsync(trackId);
            if (race == null)
            {
                throw new KeyNotFoundException();
            }
            _dbContext.Tracks.Remove(race);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int trackId, UpdateTrackDto track)
        {
            var dbTrack = await _dbContext.Tracks.FindAsync(trackId);
            if (dbTrack == null)
            {
                throw new KeyNotFoundException();
            }
            dbTrack.Name = track.Name;
            dbTrack.Number = track.Number;

            _dbContext.Tracks.Update(dbTrack);
            await _dbContext.SaveChangesAsync();
        }
    }
}
