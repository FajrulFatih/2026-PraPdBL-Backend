using Microsoft.EntityFrameworkCore;
using PraPdBL_Backend.Models;

namespace PraPdBL_Backend.Data.Seeds;

public static class RoomSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasData(
            new Room
            {
                Id = 1,
                RoomCode = "A101",
                RoomName = "Ruang Kelas A101",
                Capacity = 40,
                Location = "Gedung A Lantai 1",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1),
                UpdatedAt = new DateTime(2024, 1, 1)
            },
            new Room
            {
                Id = 2,
                RoomCode = "LAB-2",
                RoomName = "Laboratorium Komputer 2",
                Capacity = 30,
                Location = "Gedung B Lantai 2",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1),
                UpdatedAt = new DateTime(2024, 1, 1)
            }
        );
    }
}
