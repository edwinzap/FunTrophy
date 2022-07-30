using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace FunTrophy.API.Services
{
    public class ExportService : IExportService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITimeAdjustmentCategoryRepository _categoryRepository;
        private readonly ITrackOrderRepository _trackOrderRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly int PORTRAIT_MAX_WIDTH = 140;

        public ExportService(ITeamRepository teamRepository,
            ITimeAdjustmentCategoryRepository categoryRepository,
            ITrackOrderRepository trackOrderRepository,
            ITrackRepository trackRepository)
        {
            _teamRepository = teamRepository;
            _categoryRepository = categoryRepository;
            _trackOrderRepository = trackOrderRepository;
            _trackRepository = trackRepository;
        }

        #region Private methods

        private void ResizeColumns(ExcelRangeColumn columns, int[] widths)
        {
            var columnsList = columns.ToList();
            if (columnsList.Count != widths.Length)
                throw new ArgumentException("The number of columns and width are not the same");

            for (int i = 0; i < widths.Length; i++)
            {
                columnsList[i].Width = widths[i];
            }
        }

        private void FormatHeaders(ExcelRange cells)
        {
            cells.Style.Font.Size = 12;
            cells.Style.Font.Bold = true;
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void FormatTitle(ExcelRange cells)
        {
            cells.Style.Font.Size = 16;
            cells.Style.Font.Bold = true;
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void SetBorders(ExcelRange range)
        {
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        }

        #endregion Private methods

        public async Task<byte[]> GetTeamsByTimeAdjustmentCategoryFile(int raceId)
        {
            var teams = (await _teamRepository.GetOfRace(raceId))
                .OrderBy(x => x.Color.Id)
                .ThenBy(x => x.Number)
                .ToList();
            var categories = await _categoryRepository.GetOfRace(raceId);

            if (!categories.Any())
                throw new InvalidDataException("No categories");

            using var package = new ExcelPackage();
            foreach (var category in categories)
            {
                var sheet = package.Workbook.Worksheets.Add(category.Name);

                // title
                var title = sheet.Cells["A1:D1"];
                title.Merge = true;
                title.Value = category.Name;
                FormatTitle(title);

                // headers
                sheet.Cells["A2"].Value = "Couleur";
                sheet.Cells["B2"].Value = "N°";
                sheet.Cells["C2"].Value = "Nom";
                sheet.Cells["D2"].Value = "Résultat";
                FormatHeaders(sheet.Cells["A2:D2"]);

                // teams
                for (int i = 0; i < teams.Count; i++)
                {
                    var row = i + 3;
                    var team = teams[i];

                    var color = ColorTranslator.FromHtml(team.Color.Code);
                    sheet.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
                    sheet.Cells[row, 2].Value = team.Number;
                    sheet.Cells[row, 3].Value = team.Name;
                }

                sheet.Cells["B3:B100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                var range = sheet.Cells[1, 1, teams.Count + 2, 4];
                SetBorders(range);

                var widths = new[] { 10, 8, 40, 30 };
                ResizeColumns(sheet.Columns[1, 4], widths);
            }
            var fileBytes = await package.GetAsByteArrayAsync();
            return fileBytes;
        }

        public async Task<byte[]> GetTracksByColorFile(int raceId)
        {
            var teams = (await _teamRepository.GetOfRace(raceId))
                .OrderBy(x => x.Color.Id)
                .ThenBy(x => x.Number)
                .ToList();
            var colors = teams.GroupBy(x => x.Color)
                .Select(x => x.First().Color)
                .ToList();
            var tracks = await _trackRepository.GetOfRace(raceId);

            using var package = new ExcelPackage();
            foreach (var color in colors)
            {
                var sheet = package.Workbook.Worksheets.Add(color.Code);
                var teamsOfColor = teams.Where(x => x.ColorId == color.Id).ToList();
                var trackOrders = await _trackOrderRepository.GetOfColor(color.Id);

                // title
                var titleLen = 2 + trackOrders.Count;
                var title = sheet.Cells[1, 1, 1, titleLen];
                title.Merge = true;
                title.Value = color.Code;
                
                var htmlColor = ColorTranslator.FromHtml(color.Code);
                title.Style.Fill.PatternType = ExcelFillStyle.Solid;
                title.Style.Fill.BackgroundColor.SetColor(htmlColor);
                FormatTitle(title);

                sheet.Cells["A2"].Value = "N°";
                sheet.Cells["B2"].Value = "Nom";
                var index = 3;
                foreach (var trackOrder in trackOrders)
                {
                    var trackName = tracks.First(x => x.Id == trackOrder.TrackId).Name;
                    sheet.Cells[2, index++].Value = trackName;
                }
                FormatHeaders(sheet.Cells[2, 1, 2, titleLen]);

                // teams
                for (int i = 0; i < teamsOfColor.Count; i++)
                {
                    var row = i + 3;
                    var team = teamsOfColor[i];

                    sheet.Cells[row, 1].Value = team.Number;
                    sheet.Cells[row, 2].Value = team.Name;
                }

                sheet.Cells["A3:A100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                var range = sheet.Cells[1, 1, teamsOfColor.Count + 2, titleLen];
                SetBorders(range);

                var widths = new[] { 8, 25 };
                var trackWidth = (PORTRAIT_MAX_WIDTH - (8 + 25)) / trackOrders.Count;
                var trackWidths = Enumerable.Repeat(trackWidth, trackOrders.Count).ToArray();
                widths = widths.Concat(trackWidths).ToArray();
                
                ResizeColumns(sheet.Columns[1, titleLen], widths);
            }         

            var fileBytes = await package.GetAsByteArrayAsync();
            return fileBytes;
        }
    }
}