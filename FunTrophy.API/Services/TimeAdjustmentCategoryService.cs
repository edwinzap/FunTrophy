using FunTrophy.API.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;
using FunTrophy.API.Contracts.Mappers;

namespace FunTrophy.API.Services
{
    public class TimeAdjustmentCategoryService : ServiceBase, ITimeAdjustmentCategoryService
    {
        private readonly ITimeAdjustmentCategoryRepository _repository;
        private readonly ITimeAdjustmentCategoryMapper _mapper;

        public TimeAdjustmentCategoryService(ITimeAdjustmentCategoryRepository repository, ITimeAdjustmentCategoryMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> Create(AddTimeAdjustmentCategoryDto category)
        {
            var dbCategory = _mapper.Map(category);
            return _repository.Add(dbCategory);
        }

        public async Task<List<TimeAdjustmentCategoryDto>> GetAll(int raceId)
        {
            var dbCategories = await _repository.GetOfRace(raceId);
            return _mapper.Map(dbCategories);
        }

        public async Task Remove(int categoryId)
        {
            await _repository.Remove(categoryId);
        }

        public async Task Update(int categoryId, UpdateTimeAdjustmentCategoryDto category)
        {
            var dbCategory = await _repository.Get(categoryId);
            dbCategory.Name = category.Name;
            dbCategory.Description = category.Description;
            await _repository.Update(dbCategory);
        }
    }
}