using Microsoft.EntityFrameworkCore;
using PraPdBL_Backend.Models;

namespace PraPdBL_Backend.Data.Seeds;

public static class BookingStatusSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingStatus>().HasData(
            new BookingStatus
            {
                Id = 1,
                Code = "PENDING",
                Label = "Pending",
                CreatedAt = new DateTime(2024, 1, 1)
            },
            new BookingStatus
            {
                Id = 2,
                Code = "APPROVED",
                Label = "Approved",
                CreatedAt = new DateTime(2024, 2, 2)
            },
            new BookingStatus
            {
                Id = 3,
                Code = "REJECTED",
                Label = "Rejected",
                CreatedAt = new DateTime(2024, 3, 3)
            }
        );
    }
}
