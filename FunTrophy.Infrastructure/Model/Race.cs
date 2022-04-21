namespace FunTrophy.Infrastructure.Model
{
    public class Race : EntityBase
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public List<Team> Teams { get; set; }
        public List<Track> Tracks { get; set; }
    }
}