﻿using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class RaceService : ServiceBase, IRaceService
    {
        public RaceService(HttpClient httpClient) : base(httpClient, "Race")
        {
        }

        public async Task Add(AddOrUpdateRaceDto race)
        {
            var url = GetUrl();
            await PostAsync(url, race);
        }

        public async Task<List<RaceDto>> GetRaces()
        {
            var url = GetUrl();
            return await GetAsync<List<RaceDto>>(url);
        }

        public Task Remove(int raceId)
        {
            var url = GetUrl() + "/" + raceId;
            return DeleteAsync(url);
        }

        public Task Update(AddOrUpdateRaceDto race)
        {
            throw new NotImplementedException();
        }
    }
}