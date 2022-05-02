namespace FunTrophy.Shared.Model
{
    public class TrackOrderDto
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public TrackDto Track { get; set; }
        public int SordOrder { get; set; }
    }
}