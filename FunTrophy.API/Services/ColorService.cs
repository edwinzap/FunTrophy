using FunTrophy.API.Mappers;
using FunTrophy.API.Services.Contracts;
using FunTrophy.Infrastructure;
using FunTrophy.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FunTrophy.API.Services
{
    public class ColorService : ServiceBase, IColorService
    {
        private readonly IColorMapper _mapper;

        public ColorService(FunTrophyContext dbContext, IColorMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<int> Create(AddOrUpdateColorDto color)
        {
            var dbColor = _mapper.Map(color);
            _dbContext.Colors.Add(dbColor);
            await _dbContext.SaveChangesAsync();

            return dbColor.Id;
        }

        public Task<List<ColorDto>> GetAll()
        {
            var task = _dbContext.Colors.Select(x => _mapper.Map(x)).ToListAsync();
            return task;
        }

        public async Task Remove(int colorId)
        {
            var race = await _dbContext.Colors.FindAsync(colorId);
            if (race == null)
            {
                throw new KeyNotFoundException();
            }
            _dbContext.Colors.Remove(race);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int colorId, AddOrUpdateColorDto color)
        {
            var dbColor = await _dbContext.Colors.FindAsync(colorId);
            if (dbColor == null)
            {
                throw new KeyNotFoundException();
            }
            dbColor.Code = color.Code;

            _dbContext.Colors.Update(dbColor);
            await _dbContext.SaveChangesAsync();
        }
    }
}
