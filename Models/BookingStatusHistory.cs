using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PraPdBL_Backend.Models;

[Table("BookingStatusHistory")]
public class BookingStatusHistory
{
    public int Id { get; set; }

    public int BookingId { get; set; }
    public int OldStatus { get; set; }
    public int NewStatus { get; set; }
    public int ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; }
    public string? Note { get; set; }

    // Navigation
    public Booking Booking { get; set; } = null!;
    public User ChangedByUser { get; set; } = null!;
}