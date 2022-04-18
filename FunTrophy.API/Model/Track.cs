namespace FunTrophy.API.Model
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RaceId { get; set; }

        public Race Race { get; set; }
        public List<TrackOrder> TrackOrders { get; set; }
        public List<TrackTime> TrackTimes { get; set; }
    }
}