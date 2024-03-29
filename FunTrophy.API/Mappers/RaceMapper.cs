﻿using FunTrophy.API.Contracts.Mappers;
using FunTrophy.Infrastructure.Model;
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
                Date = race.Date,
                IsEnded = race.IsEnded,
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

        public List<RaceDto> Map(List<Race> races)
        {
            return races.Select(x => Map(x)).ToList();
        }
    }
}