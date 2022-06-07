using FunTrophy.API.Contracts.Helpers;
using FunTrophy.API.Contracts.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class TimeAdjustmentService : ServiceBase, ITimeAdjustmentService
    {
        private readonly ITimeAdjustmentRepository _repository;
        private readonly ITimeAdjustmentMapper _mapper;
        private readonly INotificationHelper _notificationHelper;

        public TimeAdjustmentService(
            ITimeAdjustmentRepository repository,
            ITimeAdjustmentMapper mapper,
            INotificationHelper notificationHelper)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationHelper = notificationHelper;
        }

        public async Task<int> Create(AddTimeAdjustmentDto timeAdjustment)
        {
            var dbTimeAdjustment = _mapper.Map(timeAdjustment);
            var id = await _repository.Add(dbTimeAdjustment);
            await _notificationHelper.NotifyTimeAdjustementChanged(timeAdjustment.TeamId, timeAdjustment.CategoryId);
            return id;
        }

        public async Task<List<TimeAdjustmentDto>> GetAllOfTeam(int teamId)
        {
            var dbTimeAdjustments = await _repository.GetOfTeam(teamId);
            return _mapper.Map(dbTimeAdjustments);
        }

        public async Task Remove(int timeAdjustmentId)
        {
            var timeAdjustment = await _repository.Get(timeAdjustmentId);
            await _repository.Remove(timeAdjustmentId);
            await _notificationHelper.NotifyTimeAdjustementChanged(timeAdjustment.TeamId, timeAdjustment.CategoryId);
        }
    }
}