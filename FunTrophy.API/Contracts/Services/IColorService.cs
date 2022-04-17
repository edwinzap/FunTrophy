using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services.Contracts
{
    public interface IColorService
    {
        Task<List<ColorDto>> GetAll();

        Task<int> Create(AddOrUpdateColorDto color);

        Task Remove(int colorId);

        Task Update(int colorId, AddOrUpdateColorDto color);
    }
}
