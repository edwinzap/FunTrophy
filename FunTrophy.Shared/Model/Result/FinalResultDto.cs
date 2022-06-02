namespace FunTrophy.Shared.Model
{
    public class FinalResultDto
    {
        public TeamDto Team { get; set; }
        public TimeSpan? TracksTotalDuration { get; set; }
        public TimeSpan? TimeAdjustmentsTotalDuration { get; set; }

        public TimeSpan? TotalDuration => GetTotalDuration();

        private TimeSpan? GetTotalDuration()
        {
            if (!TracksTotalDuration.HasValue)
                return null;

            if (!TimeAdjustmentsTotalDuration.HasValue)
                return TracksTotalDuration - TimeAdjustmentsTotalDuration;

            return TracksTotalDuration.Value;
        }
    }
}