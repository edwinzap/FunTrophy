using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Pages.Editor.Color
{
    public partial class Index
    {
        public List<ColorDto> Colors { get; set; }
        public Index()
        {
            Colors = FakeModel.Colors;
        }
    }
}
