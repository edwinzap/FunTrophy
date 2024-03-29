﻿using FunTrophy.API.Contracts.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.API.Mappers;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class TrackOrderService : ServiceBase, ITrackOrderService
    {
        private readonly ITrackOrderRepository _trackOrderRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly ITrackOrderMapper _mapper;

        public TrackOrderService(
            ITrackOrderRepository trackOrderRepository,
            IColorRepository colorRepository,
            ITrackRepository trackRepository,
            ITrackOrderMapper mapper
            )
        {
            _trackOrderRepository = trackOrderRepository;
            _colorRepository = colorRepository;
            _trackRepository = trackRepository;
            _mapper = mapper;
        }

        public async Task<List<TrackOrderDto>> GetAll(int colorId)
        {
            var color = await _colorRepository.Get(colorId);
            var dbTrackOrders = await _trackOrderRepository.GetOfColor(colorId);
            var dbTracks = await _trackRepository.GetOfRace(color.RaceId);
            return _mapper.Map(colorId, dbTrackOrders, dbTracks);
        }

        public async Task Update(UpdateTracksOrderDto tracksOrder)
        {
            var colorId = tracksOrder.ColorId;
            var trackIds = tracksOrder.TrackIds;

            await _trackOrderRepository.RemoveAllOfColor(colorId);
            var trackOrderList = trackIds.Select((x, index) => new TrackOrder
            {
                ColorId = colorId,
                TrackId = x,
                SortOrder = index
            });
            await _trackOrderRepository.Add(trackOrderList);
        }
    }
}