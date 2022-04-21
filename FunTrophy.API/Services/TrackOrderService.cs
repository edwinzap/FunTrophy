using FunTrophy.API.Mappers;
using FunTrophy.API.Services.Contracts;
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

        public Task<List<TrackOrderDto>> GetAll(int colorId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int trackOrderId, int sortOrder)
        {
            var dbTrackOrder = await _repository.Get(trackOrderId);
            if (dbTrackOrder.SortOrder == sortOrder)
            {
                return;
            }

            //Todo: continue
        }
    }
}