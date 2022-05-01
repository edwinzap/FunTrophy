﻿using FunTrophy.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Editor.Team
{
    public partial class Index
    {
        #region Properties
        private List<TeamDto> _allTeams;
        public List<TeamDto> Teams { get; set; }
        public List<ColorDto> Colors { get; set; }

        public int NewTeamNumber => Teams.OrderBy(x => x.Number).Select(x => x.Number).LastOrDefault() + 1;

        private int currentColorId;

        public int CurrentColorId
        {
            get { return currentColorId; }
            set
            {
                currentColorId = value;
                UpdateTeamsFilter();
            }
        }
        #endregion

        public Index()
        {
            _allTeams = FakeModel.Teams;
            Colors = FakeModel.Colors;
            CurrentColorId = FakeModel.Colors[0].Id;
        }

        private void UpdateTeamsFilter()
        {
            Teams = _allTeams.Where(x => x.Color.Id == CurrentColorId).ToList();
        }

        public void OnCurrentColorChanged(ChangeEventArgs args)
        {
            CurrentColorId = int.Parse(args.Value!.ToString()!);
        }
    }
}