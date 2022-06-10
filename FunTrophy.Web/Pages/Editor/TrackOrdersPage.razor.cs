using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Models;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TrackOrdersPage
    {
        #region Properties

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        private ITrackOrderService TrackOrderService { get; set; } = default!;

        [Inject]
        private IColorService ColorService { get; set; } = default!;

        private List<ColorDto>? Colors { get; set; }

        private List<DraggableItem<TrackDto>>? TrackOrders { get; set; }

        private int? CurrentColorId { get; set; }

        private DraggableItem<TrackDto>? draggingItem;
        private int? _selectedRaceId;

        private SaveStatus CurrentSaveStatus { get; set; } = SaveStatus.NotSaved;

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            var selectedRace = await AppStateService.GetEditorSelectedRace();
            _selectedRaceId = selectedRace?.Id;
            await LoadColors();
            await LoadTrackOrders();
        }

        private async Task LoadColors()
        {
            if (!_selectedRaceId.HasValue)
                return;

            Colors = await ColorService.GetColors(_selectedRaceId.Value);
            if (Colors.Any())
            {
                CurrentColorId = Colors.First().Id;
            }
        }

        private async Task LoadTrackOrders()
        {
            if (!CurrentColorId.HasValue)
                return;

            TrackOrders = null;
            var tracks = await TrackOrderService.GetTrackOrders(CurrentColorId.Value);
            TrackOrders = tracks
                .Select((x, index) => new DraggableItem<TrackDto>
                {
                    Item = x,
                    Order = index
                }).ToList();
            CurrentSaveStatus = SaveStatus.NotSaved;
        }

        private async Task OnCurrentColorChanged(int colorId)
        {
            CurrentColorId = colorId;
            await LoadTrackOrders();
        }

        private async Task UpdateTracksOrder()
        {
            if (TrackOrders?.Any() != true || !CurrentColorId.HasValue)
                return;

            CurrentSaveStatus = SaveStatus.Saving;
            await TrackOrderService.Update(new UpdateTracksOrderDto
            {
                ColorId = CurrentColorId.Value,
                TrackIds = TrackOrders.OrderBy(x => x.Order).Select(x => x.Item.Id).ToList()
            });
            CurrentSaveStatus = SaveStatus.Saved;
        }

        private void HandleDrop(DraggableItem<TrackDto> landingItem)
        {
            if (draggingItem is null || TrackOrders?.Any() != true)
                return;

            if (draggingItem == landingItem)
            {
                landingItem.IsDragOver = false;
                return;
            }

            int originalOrderLanding = landingItem.Order > draggingItem.Order ? landingItem.Order + 1 : landingItem.Order;

            TrackOrders.Where(x => x.Order >= originalOrderLanding).ToList().ForEach(x => x.Order++);
            draggingItem.Order = originalOrderLanding;

            int i = 0;
            foreach (var item in TrackOrders.OrderBy(x => x.Order).ToList())
            {
                item.Order = i++;
                item.IsDragOver = false;
            }
            CurrentSaveStatus = SaveStatus.NotSaved;
        }

        private enum SaveStatus
        {
            Saving,
            Saved,
            NotSaved,
        }
    }
}