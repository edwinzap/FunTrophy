﻿using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public interface ITimeAdjustmentMapper
    {
        TimeAdjustment Map(AddTimeAdjustmentDto team);

        TimeAdjustmentDto Map(TimeAdjustment team);

        List<TimeAdjustmentDto> Map(List<TimeAdjustment> teams);
    }
}