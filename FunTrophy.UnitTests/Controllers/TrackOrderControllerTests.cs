using FluentAssertions;
using FunTrophy.API.Controllers;
using FunTrophy.API.Services;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Shared.Model;
using FunTrophy.Tests.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace FunTrophy.API.UnitTests.Controllers
{
    public class TrackOrderControllerTests
    {
        private Mock<ITrackOrderService> _fakeTrackOrderService;

        public TrackOrderControllerTests()
        {
            _fakeTrackOrderService = new Mock<ITrackOrderService>();
        }

        private TrackOrderController Sut => new
            (
            new Mock<ILogger<TrackOrderController>>().Object,
            _fakeTrackOrderService.Object
            );

        #region CreateTrackOrder

        [Fact]
        public async Task CreateTrackOrder_TrackOrderDto_ServiceCreate()
        {
            var trackOrder = Some.Generated<AddTrackOrderDto>();

            await Sut.CreateTrackOrder(trackOrder);

            _fakeTrackOrderService.Verify(x => x.Create(trackOrder));
        }

        [Fact]
        public async Task CreateTrackOrder_GotTrackOrderId_ReturnTaskWithId()
        {
            var id = Some.Int();
            _fakeTrackOrderService.Setup(x => x.Create(It.IsAny<AddTrackOrderDto>()))
                .ReturnsAsync(id);

            var result = await Sut.CreateTrackOrder(Some.Generated<AddTrackOrderDto>());

            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be(id);
        }

        #endregion CreateTrackOrder
    }
}