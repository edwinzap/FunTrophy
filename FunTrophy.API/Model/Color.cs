namespace FunTrophy.API.Model
{
    public class Color
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public List<Team> Teams { get; set; }
        public List<TrackOrder> TrackOrders { get; set; }
    }
}