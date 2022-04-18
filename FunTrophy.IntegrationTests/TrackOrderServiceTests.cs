using Bogus;
using FluentAssertions;
using FunTrophy.Api.IntegrationTests.Utils;
using FunTrophy.API.Mappers;
using FunTrophy.API.Model;
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
            var color = new Faker<Color>().Generate();
            var track = new Faker<Track>().Generate();

            Arrange(ctx =>
            {
                ctx.Add(color);
                ctx.Add(track);
            });

            var trackOrderDto = new Faker<AddTrackOrderDto>()
                .RuleFor(x => x.ColorId, color.Id)
                .RuleFor(x => x.TrackId, track.Id)
                .Generate();
            
            var dbTrackOrder = new Faker<TrackOrder>().Generate();
            _fakeMapper.Setup(x => x.Map(It.IsAny<AddTrackOrderDto>()))
                .Returns(dbTrackOrder);

            var result = Sut.AddTrackOrder(It.IsAny<AddTrackOrderDto>());

            var trackOrder = _dbContext.TrackOrders.First(x => x.Id == result.Id);
            trackOrder.SortOrder.Should().Be(dbTrackOrder.SortOrder);
            trackOrder.TrackId.Should().Be(dbTrackOrder.TrackId);
            trackOrder.ColorId.Should().Be(dbTrackOrder.ColorId);
        }
    }
}