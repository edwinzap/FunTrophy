using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Pages.Editor.Team
{
    public partial class Index
    {
        private List<TeamDto> _allTeams;
        public List<TeamDto> Teams { get; set; }
        public List<ColorDto> Colors { get; set; }

        private int currentColorId;

        public int CurrentColorId
        {
            get { return currentColorId; }
            set 
            { 
                currentColorId = value; 
                OnCurrentColorChanged();
            }
        }

        public Index()
        {
            _allTeams = FakeModel.Teams;
            Colors = FakeModel.Colors;
            CurrentColorId = FakeModel.Colors[0].Id;
        }

        private void OnCurrentColorChanged()
        {
            Teams = _allTeams.Where(x => x.Color.Id == CurrentColorId).ToList();
        }
    }
}