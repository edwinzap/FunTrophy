﻿using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class RacesPage : IAsyncDisposable
    {
        #region Properties

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        private IRaceService RaceService { get; set; } = default!;

        [Inject]
        private IExportService ExportService { get; set; } = default!;

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private ConfirmDialog ResetDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        private List<RaceDto>? Races { get; set; }

        private int? DeleteRaceId { get; set; }
        private RaceDto? SelectedRace { get; set; }

        private AddOrUpdateRaceDto addRace = new() { Name = "Fun Trophy", Date = DateTime.UtcNow };

        private AddOrUpdateRaceDto updateRace = new();

        private int? updateRaceId;

        private string DownloadCategoriesFileUrl => SelectedRace is null ? string.Empty : ExportService.GetTeamsByTimeAdjustmentCategoryFileUrl(SelectedRace.Id);
        private string DownloadColorsFileUrl => SelectedRace is null ? string.Empty : ExportService.GetTracksByColorFileUrl(SelectedRace.Id);
        private string DownloadCategoriesByColorFileUrl => SelectedRace is null ? string.Empty : ExportService.GetTimeAdjustmentCategoriesByColor(SelectedRace.Id);

        private Action OnEditorStateChanged => async () => await UpdateSelectedRace();

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            AppStateService.OnEditorStateChanged += OnEditorStateChanged;
            await AppStateService.StartListeningToChange();
            await UpdateSelectedRace();
            await LoadRaces();
        }

        private async Task UpdateSelectedRace()
        {
            var currentRace = await AppStateService.GetEditorSelectedRace();
            SelectedRace = currentRace;
            await LoadRaces();
            StateHasChanged();
        }

        private async Task LoadRaces()
        {
            if (SelectedRace is null)
            {
                Races = (await RaceService.GetRaces()).OrderByDescending(x => x.Date).ToList();
            }
        }

        private Task SelectRace(RaceDto race)
        {
            return AppStateService.SetEditorSelectedRace(race);
        }

        private Task ClearSelectedRace()
        {
            return AppStateService.SetEditorSelectedRace(null);
        }

        private async Task AddRace()
        {
            await RaceService.Add(addRace);
            await LoadRaces();
        }

        private void ConfirmDeleteRace(RaceDto race)
        {
            DeleteRaceId = race.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{race.Name}'?";
            DeleteDialog.Show(message);
        }

        private async Task DeleteRace(bool confirm)
        {
            if (confirm && DeleteRaceId.HasValue)
            {
                await RaceService.Remove(DeleteRaceId.Value);
                await LoadRaces();
            }
        }

        private async Task EditRace(RaceDto race)
        {
            updateRace.Name = race.Name;
            updateRace.Date = race.Date;
            updateRaceId = race.Id;
            await EditDialog.ShowAsync();
        }

        private async Task ConfirmEditRace(bool confirm)
        {
            if (confirm && updateRaceId.HasValue)
            {
                await RaceService.Update(updateRaceId.Value, updateRace);
                await LoadRaces();
            }
        }

        private async void EndRace(bool isEnded)
        {
            if (SelectedRace is not null)
            {
                await RaceService.End(SelectedRace.Id, isEnded);
                //var race = await RaceService.GetRace(SelectedRace.Id);
                //await AppStateService.SetEditorSelectedRace(race);
                StateHasChanged();
            }
        }

        private void ResetRace()
        {
            var message = "Es-tu sûr de vouloir réinitialiser la course? Cette opération ne peut pas être annulée!";
            ResetDialog.Show(message);
        }

        private async Task ConfirmResetRace(bool confirm)
        {
            if (confirm && SelectedRace?.Id is not null)
            {
                await RaceService.Reset(SelectedRace.Id);
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            AppStateService.OnEditorStateChanged -= OnEditorStateChanged;
            await AppStateService.StopListeningToChange();
            GC.SuppressFinalize(this);
        }
    }
}