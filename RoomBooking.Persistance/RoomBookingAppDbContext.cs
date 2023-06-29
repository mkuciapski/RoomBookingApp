using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain;

namespace RoomBooking.Persistance
{
    public class RoomBookingAppDbContext : DbContext
    {
        public RoomBookingAppDbContext(DbContextOptions<RoomBookingAppDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBookingData> RoomBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().HasData(
                new Room { ID = 1, Name = "Conference Room A" },
                new Room { ID = 2, Name = "Conference Room B" },
                new Room { ID = 3, Name = "Conference Room C" }
            );
        }
    }
}
