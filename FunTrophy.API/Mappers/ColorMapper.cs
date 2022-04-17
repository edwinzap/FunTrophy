using FunTrophy.API.Model;
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
    }
}