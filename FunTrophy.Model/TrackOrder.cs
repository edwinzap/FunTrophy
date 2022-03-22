namespace FunTrophy.Model
{
    public class TrackOrder
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int ColorId { get; set; }
        public int TrackId { get; set; }

        public virtual Color Color { get; set; }
        public virtual Track Track { get; set; }
    }
}