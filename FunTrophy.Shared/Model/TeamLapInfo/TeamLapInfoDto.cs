using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTrophy.Shared.Model
{
    public class TeamLapInfoDto
    {
        public TeamDto Team { get; set; }
        public TrackDto? CurrentTrack { get; set; }

        public TrackDto? NextTrack { get; set; }
        public DateTime? CurrentTrackStartTime { get; set; }
    }
}
