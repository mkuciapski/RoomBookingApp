using RoomBooking.App.Models;

namespace RoomBooking.App.Services
{
    public interface IRoomBookingService
    {
        void Save(RoomBookingData roomBooking);

        IEnumerable<Room> GetAvailableRooms(DateTime date);
    }
}
