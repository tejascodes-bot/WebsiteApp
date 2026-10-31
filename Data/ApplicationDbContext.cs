using Microsoft.EntityFrameworkCore;
using WebsiteApp.Models;

namespace WebsiteApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms => Set<Room>();

        public DbSet<RoomType> RoomTypes => Set<RoomType>();

        public DbSet<Booking> Bookings => Set<Booking>();

        public DbSet<Guest> Guests => Set<Guest>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // RoomType - Room relationship
            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Guest - Booking relationship
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Guest)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            // RoomType - Booking relationship
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.RoomType)
                .WithMany(rt => rt.Bookings)
                .HasForeignKey(b => b.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Room - Booking relationship
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique physical room number
            modelBuilder.Entity<Room>()
                .HasIndex(r => r.RoomNumber)
                .IsUnique();

            // Unique booking confirmation
            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.ConfirmationId)
                .IsUnique();

            // Decimal precision
            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RoomType>()
                .Property(r => r.WeekdayPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RoomType>()
                .Property(r => r.WeekendPrice)
                .HasPrecision(10, 2);
        }
    }
}