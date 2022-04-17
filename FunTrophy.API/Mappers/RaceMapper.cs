using FunTrophy.API.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class RaceMapper : IRaceMapper
    {
        public RaceDto Map(Race race)
        {
            return new RaceDto
            {
                Id = race.Id,
                Name = race.Name,
                Date = race.Date
            };
        }

        public Race Map(AddOrUpdateRaceDto race)
        {
            return new Race
            {
                Name = race.Name,
                Date = race.Date
            };
        }
    }
}