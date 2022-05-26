using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TrackTimeRepository : RepositoryBase<TrackTime>, ITrackTimeRepository
    {
        public TrackTimeRepository(FunTrophyContext dbContext) : base(dbContext)
        {
        }

        public Task<List<TrackTime>> GetOfColor(int colorId)
        {
            return GetAll(x => x.Team.ColorId == colorId);
        }
    }
}
