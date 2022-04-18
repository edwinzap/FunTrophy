using Bogus;
using FluentAssertions;
using FunTrophy.Api.IntegrationTests.Utils;
using FunTrophy.API.Mappers;
using FunTrophy.API.Services;
using FunTrophy.Infrastructure;
using FunTrophy.Shared.Model;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace FunTrophy.Api.IntegrationTests
{
    public class TrackOrderServiceTests : ServiceTest<TrackOrderService>
    {
        private readonly Mock<ITrackOrderMapper> _fakeMapper;

        public TrackOrderServiceTests(FunTrophyDbIntegrationFixture fixture): base(fixture)
        {
            _fakeMapper = new Mock<ITrackOrderMapper>();
        }

        protected override Func<FunTrophyContext, TrackOrderService> CreateSut => x => new TrackOrderService(
                x, 
                _fakeMapper.Object);

        [Fact]
        public void AddTrackOrder_TrackOrder_Add()
        {
            var trackOrderDto = new Faker<AddTrackOrderDto>().Generate();
            var result = Sut.AddTrackOrder(trackOrderDto);

            var trackOrder = _dbContext.TrackOrders.First(x => x.Id == result.Id);
            trackOrder.SortOrder.Should().Be(trackOrderDto.SortOrder);
            trackOrder.TrackId.Should().Be(trackOrderDto.TrackId);
            trackOrder.ColorId.Should().Be(trackOrderDto.ColorId);
        }
    }
}