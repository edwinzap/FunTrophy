using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Pages.Editor.TrackOrder
{
    public partial class Index
    {
        public List<ColorDto> Colors { get; set; }
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
            CurrentColorId = Colors[0].Id;
        }

        private void GetTrackOrdersForCurrentColor()
        {
            TrackOrders = FakeModel.TrackOrders.Where(x => x.ColorId == currentColorId).ToList();
        }

        public void OnCurrentColorChanged(int colorId)
        {
            CurrentColorId = colorId;
        }
    }
}