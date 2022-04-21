using FluentAssertions;
using FunTrophy.API.Mappers;
using FunTrophy.API.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;
using FunTrophy.Tests.Utils;
using Moq;
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
        }

        private TrackOrderService Sut => new TrackOrderService(
                _fakeRepository.Object,
                _fakeMapper.Object);

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
        public async void Create_GotId_ReturnsTaskWithId()
        {
            var id = Some.Int();
            _fakeRepository.Setup(x => x.Add(It.IsAny<TrackOrder>()))
                .ReturnsAsync(id);

            var result = Sut.Create(Some.Generated<AddTrackOrderDto>());
            var actualId = await result;
            actualId.Should().Be(id);
        }
    }
}