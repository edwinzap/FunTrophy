using FunTrophy.API.Contracts.Services;
using FunTrophy.API.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

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
    }
}