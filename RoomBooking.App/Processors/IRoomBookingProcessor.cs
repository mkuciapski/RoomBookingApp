using RoomBooking.App.Models;

namespace RoomBooking.App.Processors
{
    public interface IRoomBookingProcessor
    {
        RoomBookingResult bookRoom(RoomBookingRequest? bookingRequest);
    }
}