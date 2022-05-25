﻿using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor
{
    public partial class TeamsPage
    {
        #region Properties

        [Inject]
        private AppState AppState { get; set; } = default!;

        [Inject]
        private ITeamService TeamService { get; set; } = default!;

        [Inject]
        private IColorService ColorService { get; set; } = default!;

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        public List<TeamDto>? Teams { get; set; }

        public List<ColorDto>? Colors { get; set; }

        private int? DeleteTeamId { get; set; }

        private AddTeamDto addTeam = new();

        private UpdateTeamDto updateTeam = new();

        private int? updateTeamId;

        public int? CurrentColorId { get; set; }

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            await LoadColors();
            await LoadTeams();
        }

        private async Task LoadColors()
        {
            if (AppState.Race?.Id == null)
                return;

            Colors = await ColorService.GetColors(AppState.Race.Id);
            if (Colors.Any())
            {
                CurrentColorId = Colors.First().Id;
            }
        }

        private async Task LoadTeams()
        {
            if (CurrentColorId.HasValue)
            {
                Teams = null;
                Teams = (await TeamService.GetTeams(CurrentColorId.Value)).OrderBy(x => x.Number).ToList();
                addTeam.Number = Teams.Select(x => x.Number).OrderBy(x => x).LastOrDefault(0) + 1;
            }
        }

        private async Task OnCurrentColorChanged(int colorId)
        {
            CurrentColorId = colorId;
            await LoadTeams();
        }

        private void ConfirmDeleteTeam(TeamDto team)
        {
            DeleteTeamId = team.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{team.Name}'";
            DeleteDialog.Show(message);
        }

        private async Task AddTeam()
        {
            if (AppState.Race?.Id != null && CurrentColorId.HasValue)
            {
                addTeam.RaceId = AppState.Race.Id;
                addTeam.ColorId = CurrentColorId.Value;
                await TeamService.Add(addTeam);
                await LoadTeams();

                addTeam.Name = string.Empty;
            }
        }

        private void ConfirmEditTeam(TeamDto team)
        {
            updateTeam.Number = team.Number;
            updateTeam.Name = team.Name;
            updateTeam.Type = team.Type;
            updateTeam.ColorId = team.Color.Id;
            updateTeamId = team.Id;
            EditDialog.Show();
        }

        private async Task DeleteTeam(bool confirm)
        {
            if (confirm && DeleteTeamId.HasValue)
            {
                await TeamService.Remove(DeleteTeamId.Value);
                await LoadTeams();
            }
        }

        private async Task UpdateTeam(bool confirm)
        {
            if (confirm && updateTeamId.HasValue)
            {
                await TeamService.Update(updateTeamId.Value, updateTeam);
                await LoadTeams();
            }
        }
    }
}