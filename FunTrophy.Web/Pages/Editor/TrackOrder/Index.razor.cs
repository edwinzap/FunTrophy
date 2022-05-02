using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor.TrackOrder
{
    public partial class Index
    {
        public List<ColorDto> Colors { get; set; }
        public List<TrackDto> Tracks { get; set; }
        public List<TrackOrderDto> TrackOrders { get; set; }
        
        private int currentColorId;

        public int CurrentColorId
        {
            get { return currentColorId; }
            set
            {
                currentColorId = value;
                GetTrackOrdersForCurrentColor();
            }
        }

        public Index()
        {
            Colors = FakeModel.Colors;
            Tracks = FakeModel.Tracks;
            CurrentColorId = Tracks.Select(x => x.Id).FirstOrDefault();
        }

        private void GetTrackOrdersForCurrentColor()
        {
            TrackOrders = FakeModel.TrackOrders.Where(x => x.ColorId == currentColorId).ToList();
        }

        public void OnCurrentColorChanged(ChangeEventArgs args)
        {
            CurrentColorId = int.Parse(args.Value!.ToString()!);
        }
    }
}
