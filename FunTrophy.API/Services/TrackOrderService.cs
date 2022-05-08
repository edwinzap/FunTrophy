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

        public Task<int> Create(AddTrackOrderDto trackOrder)
        {
            var dbTrackOrder = _mapper.Map(trackOrder);
            return _trackOrderRepository.Add(dbTrackOrder);
        }

        public async Task<List<TrackOrderDto>> GetAll(int colorId)
        {
            var color = await _colorRepository.Get(colorId);
            var dbTrackOrders = await _trackOrderRepository.GetAll(colorId);
            var dbTracks = await _trackRepository.GetAll(color.RaceId);
            return _mapper.Map(colorId, dbTrackOrders, dbTracks);
        }

        public Task Remove(int trackOrderId)
        {
            return _trackOrderRepository.Remove(trackOrderId);
        }

        public async Task Update(int colorId, List<int> trackIds)
        {
            await _trackOrderRepository.RemoveAll(colorId);
            var trackOrders = trackIds.Select((x, index) => new TrackOrder
            {
                ColorId = colorId,
                TrackId = x,
                SortOrder = index
            });
            await _trackOrderRepository.Add(trackOrders);
        }
    }
}