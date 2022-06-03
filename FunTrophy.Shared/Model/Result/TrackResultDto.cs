namespace FunTrophy.Shared.Model
{
    public class TrackResultDto
    {
        public TeamDto Team { get; set; }
        public TimeSpan? LapDuration { get; set; }
    }
}