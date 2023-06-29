namespace RoomBooking.Domain
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<RoomBookingData> RoomBokings { get; set; }
    }
}