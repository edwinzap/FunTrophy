﻿using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        public TeamRepository(FunTrophyContext dbContext) : base(dbContext)
        {
            Includes = new string[] { "Color" };
        }

        public Task<List<Team>> GetOfColor(int colorId)
        {
            return GetAll(x => x.ColorId == colorId);
        }

        public Task<List<Team>> GetOfRace(int raceId)
        {
            return GetAll(x => x.Color.RaceId == raceId);
        }
    }
}