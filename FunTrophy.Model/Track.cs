namespace FunTrophy.Model
{
    public class Track
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int RaceId { get; set; }

        public virtual Race Race { get; set; }
        public virtual ICollection<TrackOrder> TrackOrders { get; set; }
        public virtual ICollection<TrackTime> TrackTimes { get; set; }
        public virtual ICollection<TimeAdjustement> TimeAdjustements { get; set; }
    }
}