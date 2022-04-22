using FunTrophy.API.Mappers;
using FunTrophy.API.Services.Contracts;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public class ColorService : ServiceBase, IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IColorMapper _mapper;

        public ColorService(IColorRepository repository, IColorMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> Create(AddColorDto color)
        {
            var dbColor = _mapper.Map(color);
            return _repository.Add(dbColor);
        }

        public async Task<List<ColorDto>> GetAll(int raceId)
        {
            var dbColors = await _repository.GetAll(raceId);
            return _mapper.Map(dbColors);
        }

        public Task Remove(int colorId)
        {
            return _repository.Remove(colorId);
        }

        public async Task Update(int colorId, UpdateColorDto color)
        {
            var dbColor = await _repository.Get(colorId);
            dbColor.Code = color.Code;
            await _repository.Update(dbColor);
        }
    }
}