using FunTrophy.API.Contracts.Mappers;
using FunTrophy.API.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Tests.Utils;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace FunTrophy.API.UnitTests
{
    public class TrackOrderServiceTests
    {
        private readonly Mock<ITrackOrderRepository> _fakeTrackOrderRepository;
        private readonly Mock<IColorRepository> _fakeColorRepository;
        private readonly Mock<ITrackRepository> _fakeTrackRepository;
        private readonly Mock<ITrackOrderMapper> _fakeMapper;

        public TrackOrderServiceTests()
        {
            _fakeTrackOrderRepository = new Mock<ITrackOrderRepository>();
            _fakeColorRepository = new Mock<IColorRepository>();
            _fakeTrackRepository = new Mock<ITrackRepository>();
            _fakeMapper = new Mock<ITrackOrderMapper>();

            _fakeTrackOrderRepository.Setup(x => x.GetOfColor(It.IsAny<int>()))
                .ReturnsAsync(Some.Generated<TrackOrder>(0, 3));
            _fakeTrackOrderRepository.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(Some.Generated<TrackOrder>());

            _fakeColorRepository.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(Some.Generated<Color>());

            _fakeTrackRepository.Setup(x => x.GetAll(It.IsAny<int>()))
                .ReturnsAsync(Some.Generated<Track>(0, 3));
        }

        private TrackOrderService Sut => new TrackOrderService(
                _fakeTrackOrderRepository.Object,
                _fakeColorRepository.Object,
                _fakeTrackRepository.Object,
                _fakeMapper.Object);

        #region GetAll

        [Fact]
        public async Task GetAll_ColorId_GetAllOfColor()
        {
            var colorId = Some.Int();

            await Sut.GetAll(colorId);

            _fakeTrackOrderRepository.Verify(x => x.GetOfColor(colorId), Times.Once);
        }

        [Fact]
        public async Task GetAll_ColorId_ColorRepositoryGet()
        {
            int colorId = Some.Int();

            await Sut.GetAll(colorId);

            _fakeColorRepository.Verify(x => x.Get(colorId), Times.Once);
        }

        [Fact]
        public async Task GetAll_ColorId_TrackOrderRepositoryGetAll()
        {
            int colorId = Some.Int();

            await Sut.GetAll(colorId);

            _fakeTrackOrderRepository.Verify(x => x.GetOfColor(colorId), Times.Once);
        }

        [Fact]
        public async Task GetAll_GotColorWithRaceId_TrackRepositoryGetAll()
        {
            var color = Some.Generated<Color>();
            _fakeColorRepository.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(color);

            await Sut.GetAll(Some.Int());

            _fakeTrackRepository.Verify(x => x.GetAll(color.RaceId), Times.Once);
        }

        [Fact]
        public async Task GetAll__ColorId_GotTracks_GotTrackOrders__Map()
        {
            var colorId = Some.Int();
            var tracks = Some.Generated<Track>(0, 3);
            var trackOrders = Some.Generated<TrackOrder>(0, 3);

            _fakeTrackRepository.Setup(x => x.GetAll(It.IsAny<int>()))
                .ReturnsAsync(tracks);
            _fakeTrackOrderRepository.Setup(x => x.GetOfColor(colorId))
                .ReturnsAsync(trackOrders);

            await Sut.GetAll(colorId);

            _fakeMapper.Verify(x => x.Map(colorId, trackOrders, tracks), Times.Once);
        }

        #endregion GetAll
    }
}