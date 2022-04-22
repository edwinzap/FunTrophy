﻿using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        public TeamRepository(FunTrophyContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Team>> GetAll(int raceId)
        {
            return GetAll(x => x.RaceId == raceId);
        }
    }
}