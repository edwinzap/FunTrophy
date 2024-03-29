﻿using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface ITimeAdjustmentService
    {
        Task Add(AddTimeAdjustmentDto timeAdjustment);

        Task<List<TimeAdjustmentDto>> GetTimeAdjustments(int teamId);

        Task Remove(int timeAdjustmentId);

        Task<List<TeamTimeAdjustmentDto>> GetTimeAdjustmentForCategory(int categoryId);
        
        Task Update(AddTimeAdjustmentDto updateTimeAdjustment);
    }
}