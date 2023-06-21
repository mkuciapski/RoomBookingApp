using RoomBooking.App.Models;
using RoomBooking.App.Processors;

namespace RoomBooking.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        [Fact]
        public void ShouldReturnRoomBookingRequestResponse()
        {
            //Arange
            var bookingRequest = new RoomBookingRequest
            {
                FullName = "Michał Kuciapski",
                Email = "m.kuciapski@gmail.com",
                Date = new DateTime(2023, 10, 10)
            };

            var processor = new RoomBookingProcessor();

            //Act
            var result = processor.bookRoom(bookingRequest);

            //Assert
            Assert.NotNull(result);
            //result.ShouldNotBeNull();
            Assert.Equal(result.FullName, bookingRequest.FullName);
            Assert.Equal(result.Email, bookingRequest.Email);
            Assert.Equal(result.Date, bookingRequest.Date);
        }
    }
}
