﻿using FunTrophy.API.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class TrackMapper : ITrackMapper
    {
        public Track Map(AddTrackDto track)
        {
            return new Track
            {
                Name = track.Name,
                Number = track.Number,
                RaceId = track.RaceId,
            };
        }

        public TrackDto Map(Track track)
        {
            return new TrackDto
            {
                Id = track.Id,
                Name = track.Name,
                Number = track.Number,
            };
        }
    }
}