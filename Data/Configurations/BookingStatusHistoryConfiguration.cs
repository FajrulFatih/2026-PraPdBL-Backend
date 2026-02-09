using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraPdBL_Backend.Models;

namespace PraPdBL_Backend.Data.Configurations;

public class BookingStatusHistoryConfiguration 
    : IEntityTypeConfiguration<BookingStatusHistory>
{
    public void Configure(EntityTypeBuilder<BookingStatusHistory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ChangedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

        builder.Property(x => x.Note)
            .HasMaxLength(255);

        builder.HasOne(x => x.Booking)
            .WithMany(b => b.StatusHistories)
            .HasForeignKey(x => x.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ChangedByUser)
            .WithMany(u => u.StatusHistories)
            .HasForeignKey(x => x.ChangedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
