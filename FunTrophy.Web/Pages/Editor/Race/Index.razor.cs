using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Pages.Editor.Race
{
    public partial class Index
    {
        public List<RaceDto> Races { get; set; }

        public Index()
        {
            Races = FakeModel.Races;
        }
    }
}
