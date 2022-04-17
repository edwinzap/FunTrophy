﻿using FunTrophy.API.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public interface ITrackMapper
    {
        Track Map(AddTrackDto track);

        TrackDto Map(Track track);
    }
}