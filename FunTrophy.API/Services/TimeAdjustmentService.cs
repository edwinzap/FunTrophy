using FunTrophy.API.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;
using FunTrophy.API.Contracts.Mappers;

namespace FunTrophy.API.Services
{
    public class TimeAdjustmentService : ServiceBase, ITimeAdjustmentService
    {
        private readonly ITimeAdjustmentRepository _repository;
        private readonly ITimeAdjustmentMapper _mapper;

        public TimeAdjustmentService(ITimeAdjustmentRepository repository, ITimeAdjustmentMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> Create(AddTimeAdjustmentDto timeAdjustment)
        {
            var dbTimeAdjustment = _mapper.Map(timeAdjustment);
            return _repository.Add(dbTimeAdjustment);
        }

        public async Task<List<TimeAdjustmentDto>> GetAllOfTeam(int teamId)
        {
            var dbTimeAdjustments = await _repository.GetAllOfTeam(teamId);
            return _mapper.Map(dbTimeAdjustments);
        }

        public async Task Remove(int timeAdjustmentId)
        {
            await _repository.Remove(timeAdjustmentId);
        }
    }
}