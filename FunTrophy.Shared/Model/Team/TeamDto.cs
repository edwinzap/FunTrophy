namespace FunTrophy.Shared.Model
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public ColorDto Color { get; set; }
        public TeamType Type { get; set; }
    }
}
