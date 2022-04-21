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
        private readonly Mock<ITrackOrderRepository> _fakeRepository;
        private readonly Mock<ITrackOrderMapper> _fakeMapper;

        public TrackOrderServiceTests()
        {
            _fakeRepository = new Mock<ITrackOrderRepository>();
            _fakeMapper = new Mock<ITrackOrderMapper>();

            _fakeMapper.Setup(x => x.Map(It.IsAny<AddTrackOrderDto>()))
                .Returns(Some.Generated<TrackOrder>());
            _fakeRepository.Setup(x => x.GetAll(It.IsAny<int>()))
                .ReturnsAsync(Some.Generated<TrackOrder>(0, 3));
        }

        private TrackOrderService Sut => new TrackOrderService(
                _fakeRepository.Object,
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

            _fakeRepository.Verify(x => x.Add(trackOrder), Times.Once);
        }

        [Fact]
        public async Task Create_GotId_ReturnTaskWithId()
        {
            var id = Some.Int();
            _fakeRepository.Setup(x => x.Add(It.IsAny<TrackOrder>()))
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

            _fakeRepository.Verify(x => x.GetAll(colorId), Times.Once);
        }

        [Fact]
        public async Task GetAll_GotTrackOrders_Map()
        {
            var trackOrders = Some.Generated<TrackOrder>(0, 3);
            _fakeRepository.Setup(x => x.GetAll(It.IsAny<int>()))
                .ReturnsAsync(trackOrders);

            await Sut.GetAll(Some.Int());
            _fakeMapper.Verify(x => x.Map(trackOrders), Times.Once);
        }

        [Fact]
        public async Task GetAll_GotMappedTrackOrders_ReturnTrackOrdersOfColor()
        {
            var trackOrders = Some.Generated<TrackOrderDto>(0, 3);
            _fakeMapper.Setup(x => x.Map(It.IsAny<List<TrackOrder>>()))
                .Returns(trackOrders);

            var result = await Sut.GetAll(Some.Int());

            result.Should().BeEquivalentTo(trackOrders);
        }

        #endregion GetAll
    }
}