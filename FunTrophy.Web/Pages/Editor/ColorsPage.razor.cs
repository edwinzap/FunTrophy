﻿using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class ColorsPage
    {
        #region Properties

        [Inject]
        private IAppStateService AppStateService { get; set; } = default!;

        [Inject]
        private IColorService ColorService { get; set; } = default!;

        private int? DeleteColorId { get; set; }

        private AddColorDto addColor = new() { Code = "#000" };

        private UpdateColorDto updateColor = new();
        private int? updateColorId;

        private List<ColorDto>? Colors { get; set; }

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        private int? _selectedRaceId;

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            var selectedRace = await AppStateService.GetEditorSelectedRace();
            _selectedRaceId = selectedRace?.Id;
            await LoadColors();
        }

        public async Task LoadColors()
        {
            if (_selectedRaceId.HasValue)
            {
                Colors = await ColorService.GetColors(_selectedRaceId.Value);
            }
        }

        private async Task AddColor()
        {
            if (!_selectedRaceId.HasValue)
                return;

            addColor.RaceId = _selectedRaceId.Value;

            await ColorService.Add(addColor);
            await LoadColors();
        }

        private async Task EditColor(ColorDto color)
        {
            updateColor.Code = color.Code;
            updateColorId = color.Id;
            await EditDialog.ShowAsync();
        }

        private async Task ConfirmEditColor(bool confirm)
        {
            if (confirm && updateColorId.HasValue)
            {
                await ColorService.Update(updateColorId.Value, updateColor);
                await LoadColors();
            }
        }

        private void DeleteColor(ColorDto color)
        {
            DeleteColorId = color.Id;
            var message = $"Es-tu sûr de vouloir supprimer cette couleur?";
            DeleteDialog.Show(message);
        }

        private async Task ConfirmDeleteColor(bool confirm)
        {
            if (confirm && DeleteColorId.HasValue)
            {
                await ColorService.Remove(DeleteColorId.Value);
                await LoadColors();
            }
        }
    }
}