using Microsoft.EntityFrameworkCore;
using PraPdBL_Backend.Models;
using PraPdBL_Backend.Data.Seeds;

namespace PraPdBL_Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingStatus> BookingStatuses => Set<BookingStatus>();
    public DbSet<BookingStatusHistory> BookingStatusHistories => Set<BookingStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        BookingStatusSeed.Seed(modelBuilder);
        RoomSeed.Seed(modelBuilder);
    }
}