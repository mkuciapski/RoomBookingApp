using RoomBooking.App.Models;

namespace RoomBooking.App.Processors
{
    public class RoomBookingProcessor
    {
        public RoomBookingProcessor()
        {
        }

        public RoomBookingResult bookRoom(RoomBookingRequest bookingRequest)
        {
            return new RoomBookingResult
            {
                FullName = bookingRequest.FullName,
                Email = bookingRequest.Email,
                Date = bookingRequest.Date
            };
        }
    }
}