namespace FunTrophy.Model
{
    public class Color
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<TrackOrder> TrackOrders { get; set; }
    }
}