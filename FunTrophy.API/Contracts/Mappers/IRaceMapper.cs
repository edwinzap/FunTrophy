using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Mappers
{
    public interface IRaceMapper
    {
        RaceDto Map(Race race);

        Race Map(AddOrUpdateRaceDto race);

        List<RaceDto> Map(List<Race> tracks);
    }
}