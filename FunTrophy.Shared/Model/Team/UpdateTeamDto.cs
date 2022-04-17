namespace FunTrophy.Shared.Model
{
    public class UpdateTeamDto
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public TeamType Type { get; set; }
        public int ColorId { get; set; }
    }
}