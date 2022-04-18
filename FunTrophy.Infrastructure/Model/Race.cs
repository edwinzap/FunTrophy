namespace FunTrophy.API.Model
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public List<Team> Teams { get; set; }
        public List<Track> Tracks { get; set; }
    }
}