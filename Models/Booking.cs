using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PraPdBL_Backend.Models;

[Table("Booking")]
public class Booking
{
    public int Id { get; set; }

    public int RoomId { get; set; }
    public int UserId { get; set; }
    public int StatusId { get; set; }

    public string Purpose { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; } // Soft delete

    // Navigation
    public Room Room { get; set; } = null!;
    public User User { get; set; } = null!;
    public BookingStatus Status { get; set; } = null!;
    public ICollection<BookingStatusHistory> StatusHistories { get; set; } = new List<BookingStatusHistory>();
}