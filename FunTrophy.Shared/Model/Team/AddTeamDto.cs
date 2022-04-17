namespace FunTrophy.Shared.Model
{
    public class AddTeamDto
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public TeamType Type { get; set; }
        public int ColorId { get; set; }
        public int RaceId { get; set; }
    }
}