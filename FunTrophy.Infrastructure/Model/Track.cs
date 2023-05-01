namespace FunTrophy.Infrastructure.Model
{
    public class Track : EntityBase
    {
        public string Name { get; set; }
        public int RaceId { get; set; }
        public int? Number { get; set; }

        public Race Race { get; set; }
        public List<TrackOrder> TrackOrders { get; set; }
        public List<TrackTime> TrackTimes { get; set; }
    }
}