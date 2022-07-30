using FunTrophy.API.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Tests.Utils;
using Moq;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Xunit;
using DrawingColor = System.Drawing.Color;
using Color = FunTrophy.Infrastructure.Model.Color;

namespace FunTrophy.API.UnitTests.Services
{
    public class ExportServiceTests
    {
        private readonly Mock<ITeamRepository> _fakeTeamRepository;
        private readonly Mock<ITimeAdjustmentCategoryRepository> _fakeTimeAdjustmentCategoryRepository;
        private readonly Mock<ITrackOrderRepository> _fakeTrackOrderRepository;
        private readonly Mock<ITrackRepository> _fakeTrackRepository;

        public ExportServiceTests()
        {
            _fakeTeamRepository = new Mock<ITeamRepository>();
            _fakeTimeAdjustmentCategoryRepository = new Mock<ITimeAdjustmentCategoryRepository>();
            _fakeTrackOrderRepository = new Mock<ITrackOrderRepository>();
            _fakeTrackRepository = new Mock<ITrackRepository>();
        }

        private ExportService Sut => new(
            _fakeTeamRepository.Object,
            _fakeTimeAdjustmentCategoryRepository.Object,
            _fakeTrackOrderRepository.Object,
            _fakeTrackRepository.Object
            );

        [Fact]
        public async Task GetTeamsByTimeAdjustmentCategory_RaceId_CreateFile()
        {
            var raceId = Some.Int();
            var amount = Some.Int(1,5);

            var colors = new List<Color>();
            for (int i = 0; i < amount; i++)
            {
                var color = Some.Generated<Color>();
                var drawingColor = DrawingColor.FromArgb(Some.Int(0, 255), Some.Int(0, 255), Some.Int(0, 255));
                color.Code = ColorTranslator.ToHtml(drawingColor);
                colors.Add(color);
            }

            var teams = Some.InstanceOf<Team>()
                .RuleFor(x => x.Number, () => Some.Int(1, 10))
                .RuleFor(x => x.Name, () => Some.CompanyName())
                .RuleFor(x => x.Color, () => Some.From(colors.ToArray()))
                .Generate(Some.Int(10, 50));

            var categories = Some.InstanceOf<TimeAdjustmentCategory>()
                .RuleFor(x => x.Name, () => Some.CompanyName())
                .Generate(Some.Int(1, 6));

            _fakeTeamRepository.Setup(x => x.GetOfRace(raceId))
                .ReturnsAsync(teams);
            _fakeTimeAdjustmentCategoryRepository.Setup(x => x.GetOfRace(raceId))
                .ReturnsAsync(categories);

            await Sut.GetTeamsByTimeAdjustmentCategoryFile(raceId);
            _fakeTeamRepository.Verify(x => x.GetOfRace(raceId), Times.Once);
        }
    }
}