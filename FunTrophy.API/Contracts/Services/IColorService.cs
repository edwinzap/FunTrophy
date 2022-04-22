using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services.Contracts
{
    public interface IColorService
    {
        Task<List<ColorDto>> GetAll(int raceId);

        Task<int> Create(AddColorDto color);

        Task Remove(int colorId);

        Task Update(int colorId, UpdateColorDto color);
    }
}
