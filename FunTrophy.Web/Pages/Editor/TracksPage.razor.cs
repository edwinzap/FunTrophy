using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TracksPage
    {

        public List<TrackDto> Tracks { get; set; }

        public TracksPage()
        {
            Tracks = FakeModel.Tracks;
        }
    }
}
