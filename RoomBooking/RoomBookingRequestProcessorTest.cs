using Moq;
using RoomBooking.App.Models;
using RoomBooking.App.Processors;
using RoomBooking.App.Services;
using RoomBooking.Domain;
using Shouldly;

namespace RoomBooking.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        private RoomBookingProcessor _processor;
        private RoomBookingRequest _bookingRequest;
        private Mock<IRoomBookingService> _roomBookingServiceMock;
        private List<Room> _availableRooms;

        public RoomBookingRequestProcessorTest()
        {
            _bookingRequest = new RoomBookingRequest
            {
                FullName = "Michał Kuciapski",
                Email = "m.kuciapski@gmail.com",
                Date = new DateTime(2023, 10, 10)
            };

            _availableRooms = new List<Room>() { new Room() { ID = 1 } };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _roomBookingServiceMock.Setup(q => q.GetAvailableRooms(_bookingRequest.Date.Value)).Returns(_availableRooms);

            _processor = new RoomBookingProcessor(_roomBookingServiceMock.Object);
        }

        [Fact]
        public void ShouldReturnRoomBookingRequestResponse()
        {
            //Arange

            //Act
            var result = _processor.bookRoom(_bookingRequest);

            //Assert
            Assert.NotNull(result);
            //result.ShouldNotBeNull();
            Assert.Equal(result.FullName, _bookingRequest.FullName);
            Assert.Equal(result.Email, _bookingRequest.Email);
            Assert.Equal(result.Date, _bookingRequest.Date);
        }

        [Fact]
        public void ShouldReturnExceptionForNullRequest()
        {
            var exception = Should.Throw<ArgumentException>(() => _processor.bookRoom(null));

            //exception.Message.ShouldBe("Qwa");
            //Assert.Throws<ArgumentException>(() => processor.bookRoom(null));
        }

        [Fact]
        public void ShouldSaveBookRoomRequest()
        {
            RoomBookingData savedBooking = null;
            _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBookingData>())).Callback<RoomBookingData>(booking =>
            {
                savedBooking = booking;
            });

            _processor.bookRoom(_bookingRequest);

            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBookingData>()), Times.Once);
            savedBooking.ShouldNotBeNull();
            Assert.Equal(savedBooking.FullName, _bookingRequest.FullName);
            Assert.Equal(savedBooking.Email, _bookingRequest.Email);
            Assert.Equal(savedBooking.Date, _bookingRequest.Date);
            Assert.Equal(savedBooking.RoomID, _availableRooms.First().ID);
        }

        [Fact]
        public void ShouldNotSaveBookRoomRequestIfNoneAvailable()
        {
            _availableRooms.Clear();
            _processor.bookRoom(_bookingRequest);
            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBookingData>()), Times.Never);
        }

        [Theory]
        [InlineData(BookingResultFlag.Failure, false)]
        [InlineData(BookingResultFlag.Success, true)]
        public void ShouldReturnFlagInResult(BookingResultFlag bookingSuccessFlag, bool isAvailable)
        {
            if (!isAvailable)
                _availableRooms.Clear();

            var result = _processor.bookRoom(_bookingRequest);
            bookingSuccessFlag.ShouldBe(result.Flag);
        }


        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void ShouldReturnRoomBookinINResult(int? roomBookingID, bool isAvailable)
        {
            if (!isAvailable)
                _availableRooms.Clear();

            _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBookingData>())).Callback<RoomBookingData>(booking =>
            {
                booking.ID = roomBookingID.Value;
            });

            var result = _processor.bookRoom(_bookingRequest);
            roomBookingID.ShouldBe(result.ID);
        }
    }
}
