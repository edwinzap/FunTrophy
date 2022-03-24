namespace FunTrophy.API.Model
{
    public class TrackOrder
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int ColorId { get; set; }
        public int TrackId { get; set; }

        public Color Color { get; set; }
        public Track Track { get; set; }
    }
}