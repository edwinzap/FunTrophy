using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor.Team
{
    public partial class Index
    {
        #region Properties
        public List<TeamDto> Teams { get; set; }
        public List<ColorDto> Colors { get; set; }

        public int NewTeamNumber => Teams.OrderBy(x => x.Number).Select(x => x.Number).LastOrDefault() + 1;

        private int currentColorId;

        public int CurrentColorId
        {
            get { return currentColorId; }
            set
            {
                currentColorId = value;
                GetTeamsForCurrentColor();
            }
        }
        #endregion

        public Index()
        {
            Colors = FakeModel.Colors;
            CurrentColorId = FakeModel.Colors[0].Id;
        }

        private void GetTeamsForCurrentColor()
        {
            Teams = FakeModel.Teams.Where(x => x.Color.Id == CurrentColorId).ToList();
        }

        public void OnCurrentColorChanged(ChangeEventArgs args)
        {
            CurrentColorId = int.Parse(args.Value!.ToString()!);
        }
    }
}