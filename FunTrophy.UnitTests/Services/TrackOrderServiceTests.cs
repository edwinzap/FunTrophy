using FluentAssertions;
using FunTrophy.API.Mappers;
using FunTrophy.API.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;
using FunTrophy.Tests.Utils;
using Moq;
using System.Collections.Generic;
using System.Linq;
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
            _fakeRepository.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(Some.Generated<TrackOrder>());
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

        #region Update

        [Fact]
        public async Task Update_TrackOrderId_RepositoryGet()
        {
            var trackOrderId = Some.Int();

            await Sut.Update(trackOrderId, Some.Int());

            _fakeRepository.Verify(x => x.Get(trackOrderId), Times.Once);
        }

        [Fact]
        public async Task Update_GotTrackOrderWithSameSortOrder_DoesNotContinue()
        {
            var sortOrder = Some.Int();
            var trackOrder = Some.InstanceOf<TrackOrder>()
                .RuleFor(x => x.SortOrder, sortOrder)
                .Generate();
            _fakeRepository.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(trackOrder);

            await Sut.Update(Some.Int(), sortOrder);

            _fakeRepository.Verify(x => x.GetAll(It.IsAny<int>()), Times.Never);
            _fakeMapper.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Update_GotTrackOrder_RepositoryGetAll()
        {
            var colorId = Some.Int();
            var trackOrder = Some.InstanceOf<TrackOrder>()
                .RuleFor(x => x.SortOrder, Some.Int())
                .RuleFor(x => x.ColorId, colorId)
                .Generate();
            _fakeRepository.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(trackOrder);

            await Sut.Update(Some.Int(), Some.Int());

            _fakeRepository.Verify(x => x.GetAll(colorId), Times.Once);
        }

        public static IEnumerable<object[]> SortOrder_TrackOrders()
        {
            var index = 1;
            var actualTrackOrders = Some.InstanceOf<TrackOrder>()
                .RuleFor(x => x.Id, f => index)
                .RuleFor(x => x.SortOrder, f => index++)
                .Generate(5);

            var startIndex_1 = 0;
            var expectedOrder_1 = new int[] { 2, 1, 3, 4, 5 };
            var expectedTrackOrders_1 = CloneWithSortOrder(actualTrackOrders, expectedOrder_1);
            yield return new object[] { actualTrackOrders, expectedTrackOrders_1, startIndex_1 };

            var startIndex_2 = 4;
            var expectedOrder_2 = new int[] { 1, 2, 4, 5, 3 };
            var expectedTrackOrders_2 = CloneWithSortOrder(actualTrackOrders, expectedOrder_2);
            yield return new object[] { actualTrackOrders, expectedTrackOrders_2, startIndex_2 };

            var startIndex_3 = 1;
            var expectedOrder_3 = new int[] { 1, 4, 2, 3, 5 };
            var expectedTrackOrders_3 = CloneWithSortOrder(actualTrackOrders, expectedOrder_3);
            yield return new object[] { actualTrackOrders, expectedTrackOrders_3, startIndex_3 };

        }

        private static List<TrackOrder> CloneWithSortOrder(List<TrackOrder> trackOrders, int[] targetSortOrder)
        {
            var newTrackOrders = new List<TrackOrder>();
            for (int i = 0; i < targetSortOrder.Length; i++)
            {
                var trackOrder = trackOrders[i];
                var newTrackOrder = Some.InstanceOf<TrackOrder>()
                    .RuleFor(x => x.Id, trackOrder.Id)
                    .RuleFor(x => x.SortOrder, targetSortOrder[i])
                    .Generate();
                newTrackOrders.Add(newTrackOrder);
            }
            return newTrackOrders;
        }

        [Theory, MemberData(nameof(SortOrder_TrackOrders))]
        public async Task Update_GotAllTrackOrders_UpdateEachSortOrder(List<TrackOrder> actualTrackOrders, List<TrackOrder> expectedTrackOrders, int index)
        {
            var trackOrder = actualTrackOrders[index];

            _fakeRepository.Setup(x => x.Get(trackOrder.Id))
                .ReturnsAsync(trackOrder);
            _fakeRepository.Setup(x => x.GetAll(It.IsAny<int>()))
                .ReturnsAsync(actualTrackOrders);

            IEnumerable<TrackOrder>? resultTrackOrders = null;
            _fakeRepository.Setup(x => x.Update(It.IsAny<IEnumerable<TrackOrder>>()))
                .Callback<IEnumerable<TrackOrder>>(x => resultTrackOrders = x);

            await Sut.Update(trackOrder.Id, expectedTrackOrders[index].SortOrder);

            resultTrackOrders!.Select(x => x.SortOrder).Should().BeEquivalentTo(expectedTrackOrders.Select(x => x.SortOrder));
        }

        #endregion Update
    }
}