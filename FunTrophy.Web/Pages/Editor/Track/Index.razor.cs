using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Pages.Editor.Track
{
    public partial class Index
    {

        public List<TrackDto> Tracks { get; set; }

        public Index()
        {
            Tracks = FakeModel.Tracks;
        }
    }
}
