using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain;
using RoomBooking.Persistance.Services;
using Xunit;

namespace RoomBooking.Persistance.Tests
{
    public class RoomBookingServiceTest
    {
        [Fact]
        public void ShouldReturnAvailableRooms()
        {
            //Arange
            var date = new DateTime(2023, 10, 10);

            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>().UseInMemoryDatabase("AvailableRoomTest").Options;
            using (var context = new RoomBookingAppDbContext(dbOptions))
            {
                context.Add(new Room() { ID = 1, Name = "Mordor" });
                context.Add(new Room() { ID = 2, Name = "Milano" });

                context.Add(new RoomBookingData() { RoomID = 1, Date = date });
                context.Add(new RoomBookingData() { RoomID = 2, Date = date.AddDays(-1) });

                context.SaveChanges();

                var roomBookingService = new RoomBookingService(context);

                //Act
                var availableRooms = roomBookingService.GetAvailableRooms(date);

                //Assert

                Assert.Equal(1, availableRooms.Count());
                Assert.Contains(availableRooms, q => q.ID == 2);
                Assert.DoesNotContain(availableRooms, q => q.ID == 1);
            }
        }

        [Fact]
        public void ShouldSaveBooking()
        {
            //Arange
            var date = new DateTime(2023, 10, 10);

            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>().UseInMemoryDatabase("AvailableRoomTest").Options;
            using (var context = new RoomBookingAppDbContext(dbOptions))
            {
                var newRoomBooking = new RoomBookingData() { RoomID = 1, Date = date };
                var roomBookingService = new RoomBookingService(context);

                //Act
                roomBookingService.Save(newRoomBooking);

                //Assert

                Assert.Equal(1, context.RoomBookings.Count());
                var booking = Assert.Single(context.RoomBookings);

                Assert.Equal(newRoomBooking.Date, booking.Date);
                Assert.Equal(newRoomBooking.RoomID, booking.RoomID);
            }
        }

    }
}
