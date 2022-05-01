using FunTrophy.Shared.Model;

namespace FunTrophy.Web
{
    public static class FakeModel
    {
        public static List<RaceDto> Races { get; } = new List<RaceDto>()
        {
            new RaceDto() { Id = 1, Name = "FunTrophy 2020", Date = new DateTime(2020, 6, 25)},
            new RaceDto() { Id = 2, Name = "FunTrophy 2021", Date = new DateTime(2021, 6, 25)},
            new RaceDto() { Id = 3, Name = "FunTrophy 2022", Date = new DateTime(2022, 6, 25)},
        };

        public static List<ColorDto> Colors { get; } = new List<ColorDto>()
        {
            new ColorDto { Id = 1, Code = "#eb4034"},
            new ColorDto { Id = 2, Code = "#ebb434"},
            new ColorDto { Id = 3, Code = "#4feb34"},
            new ColorDto { Id = 4, Code = "#34e8eb"},
            new ColorDto { Id = 5, Code = "#3452eb"},
            new ColorDto { Id = 6, Code = "#c034eb"},
        };

        public static List<TeamDto> Teams { get; } = new List<TeamDto>
        {
            new TeamDto { Id = 1, Name= "Emmerich, Schumm and Murray", Number = 1, Type = TeamType.Family, Color = Colors[0]},
            new TeamDto { Id = 2, Name= "Powlowski-Stiedemann", Number = 2, Type = TeamType.Family, Color = Colors[0]},
            new TeamDto { Id = 3, Name= "Balistreri Inc", Number = 3, Type = TeamType.Family, Color = Colors[0]},

            new TeamDto { Id = 4, Name= "Lowe LLC", Number = 1, Type = TeamType.Family, Color = Colors[1]},
            new TeamDto { Id = 5, Name= "Ebert-Hills", Number = 2, Type = TeamType.Family, Color = Colors[1]},
            new TeamDto { Id = 6, Name= "Rowe, Rowe and Roberts", Number = 3, Type = TeamType.Family, Color = Colors[1]},
        };
    }
}