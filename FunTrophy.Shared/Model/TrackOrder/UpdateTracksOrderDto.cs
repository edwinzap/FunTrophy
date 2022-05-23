namespace FunTrophy.Shared.Model
{
    public class UpdateTracksOrderDto
    {
        public int ColorId { get; set; }
        public List<int> TrackIds { get; set; }
    }
}