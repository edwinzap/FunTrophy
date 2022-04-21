using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class ColorMapper : IColorMapper
    {
        public ColorDto Map(Color color)
        {
            return new ColorDto
            {
                Id = color.Id,
                Code = color.Code
            };
        }

        public Color Map(AddOrUpdateColorDto color)
        {
            return new Color
            {
                Code = color.Code
            };
        }

        public List<ColorDto> Map(List<Color> colors)
        {
            return colors.Select(x => Map(x)).ToList();
        }
    }
}