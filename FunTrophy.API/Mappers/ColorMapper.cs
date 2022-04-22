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

        public Color Map(AddColorDto color)
        {
            return new Color
            {
                RaceId = color.RaceId,
                Code = color.Code
            };
        }

        public List<ColorDto> Map(List<Color> colors)
        {
            return colors.Select(x => Map(x)).ToList();
        }
    }
}