using RoomBooking.App.Services;
using RoomBooking.Domain;

namespace RoomBooking.Persistance.Services
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingAppDbContext _context;

        public RoomBookingService(RoomBookingAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {

            var availableRooms = _context.Rooms.Where(r => !r.RoomBokings.Any(b => b.Date == date));
            return availableRooms;
        }

        public void Save(RoomBookingData roomBooking)
        {
            _context.Add(roomBooking);
            _context.SaveChanges();
        }
    }
}
