namespace FunTrophy.Infrastructure.Model
{
    public class TrackOrder : EntityBase
    {
        public int SortOrder { get; set; }
        public int ColorId { get; set; }
        public int TrackId { get; set; }

        public Color Color { get; set; }
        public Track Track { get; set; }
    }
}