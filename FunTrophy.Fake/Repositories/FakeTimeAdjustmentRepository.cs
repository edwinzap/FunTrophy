﻿using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeTimeAdjustmentRepository : FakeRepositoryBase<TimeAdjustment>, ITimeAdjustmentRepository
    {
        public FakeTimeAdjustmentRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public override Task<int> Add(TimeAdjustment entity)
        {
            var category = _dbContext.TimeAdjustmentCategories.First(x => x.Id == entity.CategoryId);
            entity.Category = category;
            return base.Add(entity);
        }

        public Task<List<TimeAdjustment>> GetOfRace(int raceId)
        {
            return GetAll(x => x.Team.Color.RaceId == raceId);
        }

        public Task<List<TimeAdjustment>> GetOfTeam(int teamId)
        {
            return GetAll(x => x.TeamId == teamId);
        }
    }
}