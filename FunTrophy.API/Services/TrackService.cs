﻿using FunTrophy.API.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;
using FunTrophy.API.Contracts.Mappers;

namespace FunTrophy.API.Services
{
    public class TrackService : ServiceBase, ITrackService
    {
        private readonly ITrackRepository _repository;
        private readonly ITrackMapper _mapper;

        public TrackService(ITrackRepository repository, ITrackMapper mapper)
        {
            _repository = repository;
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

        public Task Remove(int trackId)
        {
            return _repository.Remove(trackId);
        }

        public async Task Update(int trackId, UpdateTrackDto track)
        {
            var dbTrack = await _repository.Get(trackId);
            dbTrack.Name = track.Name;
            await _repository.Update(dbTrack);
        }
    }
}