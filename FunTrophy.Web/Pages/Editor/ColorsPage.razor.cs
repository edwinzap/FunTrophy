using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class ColorsPage
    {
        public List<ColorDto> Colors { get; set; }
        public ColorsPage()
        {
            Colors = FakeModel.Colors;
        }
    }
}
