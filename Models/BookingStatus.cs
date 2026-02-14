using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PraPdBL_Backend.Models;

[Table("BookingStatus")]
public class BookingStatus
{
    public int Id { get; set; }
    public string Code { get; set; } = null!; // PENDING, APPROVED, REJECTED
    public string Label { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    // Navigation
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}