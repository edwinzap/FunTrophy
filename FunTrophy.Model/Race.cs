namespace FunTrophy.Model
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}