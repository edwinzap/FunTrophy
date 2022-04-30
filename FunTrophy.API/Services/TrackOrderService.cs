using FunTrophy.API.Contracts.Services;
using FunTrophy.API.Mappers;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class TrackOrderService : ServiceBase, ITrackOrderService
    {
        private readonly ITrackOrderRepository _repository;
        private readonly ITrackOrderMapper _mapper;

        public TrackOrderService(ITrackOrderRepository repository, ITrackOrderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> Create(AddTrackOrderDto trackOrder)
        {
            var dbTrackOrder = _mapper.Map(trackOrder);
            return _repository.Add(dbTrackOrder);
        }

        public async Task<List<TrackOrderDto>> GetAll(int colorId)
        {
            var dbTrackOrders = await _repository.GetAll(colorId);
            return _mapper.Map(dbTrackOrders);
        }

        public Task Remove(int trackOrderId)
        {
            return _repository.Remove(trackOrderId);
        }

        public async Task Update(int trackOrderId, int sortOrder)
        {
            var trackOrder = await _repository.Get(trackOrderId);
            if (trackOrder.SortOrder == sortOrder)
            {
                return;
            }

            var trackOrders = await _repository.GetAll(trackOrder.ColorId);
            var originalSortOrder = trackOrder.SortOrder;

            foreach (var item in trackOrders)
            {
                if (item.Id == trackOrderId)
                {
                    trackOrder.SortOrder = sortOrder;
                    continue;
                }

                if (originalSortOrder >= sortOrder)
                {
                    if (item.SortOrder >= sortOrder)
                    {
                        item.SortOrder++;
                    }
                }
                else
                {
                    if (item.SortOrder <= sortOrder)
                    {
                        item.SortOrder--;
                    }
                }
            }

            await _repository.Update(trackOrders);
        }
    }
}