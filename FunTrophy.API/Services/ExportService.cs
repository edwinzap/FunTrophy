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

        public ExportService(ITeamRepository teamRepository,
            ITimeAdjustmentCategoryRepository categoryRepository
            )
        {
            _teamRepository = teamRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task GetTeamsByTimeAdjustmentCategory(int raceId)
        {
            var teams = (await _teamRepository.GetOfRace(raceId))
                .OrderBy(x => x.Color.Id)
                .ThenBy(x => x.Number)
                .ToList();
            var categories = await _categoryRepository.GetOfRace(raceId);

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
                    sheet.Cells[row, 2].Value = team.Number.ToString();
                    sheet.Cells[row, 3].Value = team.Name;
                }

                sheet.Columns[1, 4].AutoFit();
                var range = sheet.Cells[2, 1, teams.Count + 2, 4];
                SetBorders(range);
            }
            await package.SaveAsAsync(new FileInfo(@"C:\Users\MFORGET\Documents\Miguel\Temp\Categories.xlsx"));
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
    }
}