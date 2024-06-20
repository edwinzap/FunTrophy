using FunTrophy.API.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;
using FunTrophy.API.Contracts.Mappers;

namespace FunTrophy.API.Services
{
    public class TrackService : ServiceBase, ITrackService
    {
        private readonly ITrackRepository _repository;
        private readonly ITrackOrderRepository _trackOrderRepository;
        private readonly ITrackMapper _mapper;

        public TrackService(ITrackRepository repository, ITrackOrderRepository trackOrderRepository, ITrackMapper mapper)
        {
            _repository = repository;
            _trackOrderRepository = trackOrderRepository;
            _mapper = mapper;
        }

        public Task<int> Create(AddTrackDto track)
        {
            var dbTrack = _mapper.Map(track);
            return _repository.Add(dbTrack);
        }

        public async Task<List<TrackDto>> GetAll(int raceId)
        {
            var dbTracks = await _repository.GetOfRace(raceId);
            return _mapper.Map(dbTracks);
        }

        public async Task Remove(int trackId)
        {
            await _trackOrderRepository.RemoveAllOfTrack(trackId);
            await _repository.Remove(trackId);
        }

        public async Task Update(int trackId, UpdateTrackDto track)
        {
            var dbTrack = await _repository.Get(trackId);
            dbTrack.Name = track.Name;
            dbTrack.Number = track.Number;
            await _repository.Update(dbTrack);
        }
    }
}