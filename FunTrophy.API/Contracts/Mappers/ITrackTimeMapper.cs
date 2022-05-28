using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Mappers;

public interface ITrackTimeMapper
{
    TeamLapInfoDto Map(TrackTime? current, TrackTime? next, Team team);
}
