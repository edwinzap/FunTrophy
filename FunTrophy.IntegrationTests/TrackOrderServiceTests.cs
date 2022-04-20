using FunTrophy.API.Mappers;
using FunTrophy.API.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using Moq;
using Xunit;

namespace FunTrophy.Api.IntegrationTests
{
    public class TrackOrderServiceTests
    {
        private readonly Mock<ITrackOrderRepository> _fakeRepository;
        private readonly Mock<ITrackOrderMapper> _fakeMapper;

        public TrackOrderServiceTests()
        {
            _fakeRepository = new Mock<ITrackOrderRepository>();
            _fakeMapper = new Mock<ITrackOrderMapper>();
        }

        private TrackOrderService Sut => new TrackOrderService(
                _fakeRepository.Object,
                _fakeMapper.Object);

        [Fact]
        public void AddTrackOrder_TrackOrder_Add()
        {
        }
    }
}