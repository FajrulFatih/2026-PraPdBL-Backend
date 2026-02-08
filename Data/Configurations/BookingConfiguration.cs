using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraPdBL_Backend.Models;

namespace PraPdBL_Backend.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(x => x.RoomId);

        builder.HasOne(x => x.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Status)
            .WithMany(s => s.Bookings)
            .HasForeignKey(x => x.StatusId);

        builder.HasQueryFilter(x => x.DeletedAt == null);
    }
}
