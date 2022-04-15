using FunTrophy.API.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public interface IMapper
    {
        RaceDto Map(Race race);
        Race Map(AddOrUpdateRaceDto race);
    }
}