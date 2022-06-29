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
        private readonly ITeamRepository _teamRepository;
        private readonly ITimeAdjustmentCategoryRepository _categoryRepository;
        private readonly ITimeAdjustmentMapper _mapper;
        private readonly INotificationHelper _notificationHelper;

        public TimeAdjustmentService(
            ITimeAdjustmentRepository repository,
            ITeamRepository teamRepository,
            ITimeAdjustmentCategoryRepository categoryRepository,
            ITimeAdjustmentMapper mapper,
            INotificationHelper notificationHelper)
        {
            _repository = repository;
            _teamRepository = teamRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _notificationHelper = notificationHelper;
        }

        public async Task<int> Create(AddTimeAdjustmentDto timeAdjustment)
        {
            if (timeAdjustment.Seconds == 0)
                throw new InvalidDataException("Seconds cannot be 0");

            var dbTimeAdjustment = _mapper.Map(timeAdjustment);
            var id = await _repository.Add(dbTimeAdjustment);
            await _notificationHelper.NotifyTimeAdjustementChanged(timeAdjustment.TeamId, timeAdjustment.CategoryId);
            return id;
        }

        public async Task<List<TeamTimeAdjustmentDto>> GetAllOfCategory(int categoryId)
        {
            var category = await _categoryRepository.Get(categoryId);
            var dbTimeAdjustments = await _repository.GetOfCategory(categoryId);
            var teams = await _teamRepository.GetOfRace(category.RaceId);

            return _mapper.MapTeamTimeAdjustment(dbTimeAdjustments, teams);
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

        public async Task<int?> Update(AddTimeAdjustmentDto timeAdjustment)
        {
            var dbTimeAdjustment = await _repository.GetByTeamAndCategory(timeAdjustment.TeamId, timeAdjustment.CategoryId);

            if (dbTimeAdjustment?.Seconds == timeAdjustment.Seconds)
                return dbTimeAdjustment.Id;

            if (dbTimeAdjustment is not null)
            {
                await _repository.Remove(dbTimeAdjustment.Id);
            }

            await _notificationHelper.NotifyTimeAdjustementChanged(timeAdjustment.TeamId, timeAdjustment.CategoryId);
            if (timeAdjustment.Seconds == 0)
                return null;

            var newTimeAdjustment = _mapper.Map(timeAdjustment);
            var id = await _repository.Add(newTimeAdjustment);
            return id;
        }
    }
}