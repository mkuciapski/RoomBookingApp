using RoomBooking.Domain.BaseModels;
using RoomBooking.Tests;

namespace RoomBooking.App.Models
{
    public class RoomBookingResult : RoomBookingBase
    {
        public BookingResultFlag Flag { get; set; }
        public int? ID { get; set; }
    }
}