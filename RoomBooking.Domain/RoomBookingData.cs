using RoomBooking.Domain.BaseModels;

namespace RoomBooking.Domain
{
    public class RoomBookingData : RoomBookingBase
    {
        public int ID { get; set; }

        public Room Room { get; set; }
        public int RoomID { get; set; }
    }
}