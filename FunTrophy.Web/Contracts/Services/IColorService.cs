using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface IColorService
    {
        Task Add(AddColorDto color);
        Task<List<ColorDto>> GetColors(int raceId);
        Task Remove(int colorId);
        Task Update(UpdateColorDto color);
    }
}