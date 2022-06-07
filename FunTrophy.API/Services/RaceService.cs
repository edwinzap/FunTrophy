using FunTrophy.API.Contracts.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class RaceService : ServiceBase, IRaceService
    {
        private readonly IRaceRepository _repository;
        private readonly IRaceMapper _mapper;
        private readonly ITrackTimeRepository _trackTimeRepository;
        private readonly ITimeAdjustmentRepository _timeAdjustmentRepository;

        public RaceService(
            IRaceRepository repository,
            IRaceMapper mapper,
            ITrackTimeRepository trackTimeRepository,
            ITimeAdjustmentRepository timeAdjustmentRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _trackTimeRepository = trackTimeRepository;
            _timeAdjustmentRepository = timeAdjustmentRepository;
        }

        public async Task<RaceDto> Get(int raceId)
        {
            var race = await _repository.Get(raceId);
            return _mapper.Map(race);
        }

        public Task<int> Create(AddOrUpdateRaceDto race)
        {
            var dbRace = _mapper.Map(race);
            return _repository.Add(dbRace);
        }

        public async Task<List<RaceDto>> GetAll()
        {
            var dbRaces = await _repository.GetAll();
            return _mapper.Map(dbRaces);
        }

        public Task Remove(int raceId)
        {
            return _repository.Remove(raceId);
        }

        public async Task Update(int raceId, AddOrUpdateRaceDto race)
        {
            var dbRace = await _repository.Get(raceId);
            dbRace.Name = race.Name;
            dbRace.Date = race.Date;
            await _repository.Update(dbRace);
        }

        public async Task End(int raceId, bool isEnded)
        {
            var dbRace = await _repository.Get(raceId);
            if (dbRace.IsEnded == isEnded) return;

            dbRace.IsEnded = isEnded;
            await _repository.Update(dbRace);
        }

        public async Task Reset(int raceId)
        {
            await _trackTimeRepository.RemoveAllOfRace(raceId);
            await _timeAdjustmentRepository.RemoveAllOfRace(raceId);
        }
    }
}