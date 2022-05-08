using DeepEqual.Syntax;
using FluentAssertions;
using FunTrophy.API.Mappers;
using FunTrophy.API.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;
using FunTrophy.Tests.Utils;
using Moq;
using System.Collections.Generic;
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

            _fakeMapper.Setup(x => x.Map(It.IsAny<AddTrackOrderDto>()))
                .Returns(Some.Generated<TrackOrder>());

            _fakeTrackOrderRepository.Setup(x => x.GetAll(It.IsAny<int>()))
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

        #region Create

        [Fact]
        public void Create_TrackOrder_Map()
        {
            var trackOrderDto = Some.Generated<AddTrackOrderDto>();

            Sut.Create(trackOrderDto);

            _fakeMapper.Verify(x => x.Map(trackOrderDto), Times.Once);
        }

        [Fact]
        public void Create_GotMappedTrackOrder_RepositoryAdd()
        {
            var trackOrder = Some.Generated<TrackOrder>();
            _fakeMapper.Setup(x => x.Map(It.IsAny<AddTrackOrderDto>()))
                .Returns(trackOrder);

            Sut.Create(Some.Generated<AddTrackOrderDto>());

            _fakeTrackOrderRepository.Verify(x => x.Add(trackOrder), Times.Once);
        }

        [Fact]
        public async Task Create_GotId_ReturnTaskWithId()
        {
            var id = Some.Int();
            _fakeTrackOrderRepository.Setup(x => x.Add(It.IsAny<TrackOrder>()))
                .ReturnsAsync(id);

            var result = Sut.Create(Some.Generated<AddTrackOrderDto>());
            var actualId = await result;
            actualId.Should().Be(id);
        }

        #endregion Create

        #region GetAll

        [Fact]
        public async Task GetAll_ColorId_GetAllOfColor()
        {
            var colorId = Some.Int();

            await Sut.GetAll(colorId);

            _fakeTrackOrderRepository.Verify(x => x.GetAll(colorId), Times.Once);
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

            _fakeTrackOrderRepository.Verify(x => x.GetAll(colorId), Times.Once);
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
            _fakeTrackOrderRepository.Setup(x => x.GetAll(colorId))
                .ReturnsAsync(trackOrders);

            await Sut.GetAll(colorId);

            _fakeMapper.Verify(x => x.Map(colorId, trackOrders, tracks), Times.Once);
        }

        #endregion GetAll

        #region Update

        [Fact]
        public async Task Update_ColorId_TrackOrderRepositoryRemoveAll()
        {
            var colorId = Some.Int();

            await Sut.Update(colorId, Some.Generated<int>(0, 3));

            _fakeTrackOrderRepository.Verify(x => x.RemoveAll(colorId), Times.Once);
        }

        [Fact]
        public async Task Update__ColorId_TrackIds__TrackOrderRepositoryAdd()
        {
            var colorId = Some.Int();
            var trackIds = Some.Generated<int>(0, 5);
            var trackOrders = new List<TrackOrder>();

            var index = 0;
            foreach (var trackId in trackIds)
            {
                trackOrders.Add(new TrackOrder
                {
                    ColorId = colorId,
                    SortOrder = index++,
                    TrackId = trackId
                });
            }

            await Sut.Update(colorId, trackIds);

            _fakeTrackOrderRepository.Verify(x => x.Add(It.Is<IEnumerable<TrackOrder>>(x => x.IsDeepEqual(trackOrders))), Times.Once);
        }

        #endregion Update
    }
}