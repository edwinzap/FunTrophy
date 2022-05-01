using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Pages.Editor.TrackOrder
{
    public partial class Index
    {
        public List<ColorDto> Colors { get; set; }
        public List<TrackDto> Tracks { get; set; }
        public Index()
        {
            Colors = FakeModel.Colors;
            Tracks = FakeModel.Tracks;
        }
    }
}
