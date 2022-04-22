using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public interface IColorMapper
    {
        Color Map(AddColorDto color);

        ColorDto Map(Color color);

        List<ColorDto> Map(List<Color> colors);
    }
}