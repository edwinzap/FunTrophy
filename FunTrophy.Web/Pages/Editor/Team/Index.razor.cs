using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Pages.Editor.Team
{
    public partial class Index
    {
        #region Properties

        public List<TeamDto> Teams { get; set; }
        public List<ColorDto> Colors { get; set; }

        public int NewTeamNumber => Teams.OrderBy(x => x.Number).Select(x => x.Number).LastOrDefault() + 1;

        private int _currentColorId;

        public int CurrentColorId
        {
            get => _currentColorId;
            set
            {
                _currentColorId = value;
                GetTeamsForCurrentColor();
            }
        }

        #endregion Properties

        public Index()
        {
            Colors = FakeModel.Colors;
            CurrentColorId = Colors[0].Id;
        }

        private void GetTeamsForCurrentColor()
        {
            Teams = FakeModel.Teams.Where(x => x.Color.Id == CurrentColorId).ToList();
        }

        public void OnCurrentColorChanged(int colorId)
        {
            CurrentColorId = colorId;
        }
    }
}