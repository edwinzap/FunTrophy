using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public interface IRaceMapper
    {
        RaceDto Map(Race race);
        Race Map(AddOrUpdateRaceDto race);
    }
}