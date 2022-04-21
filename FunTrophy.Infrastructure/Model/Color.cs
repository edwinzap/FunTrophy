namespace FunTrophy.Infrastructure.Model
{
    public class Color : EntityBase
    {
        public string Code { get; set; }

        public List<Team> Teams { get; set; }
        public List<TrackOrder> TrackOrders { get; set; }
    }
}