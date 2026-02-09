using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PraPdBL_Backend.Models;

[Table("Room")]
public class Room
{
    public int Id { get; set; }
    public string RoomCode { get; set; } = null!;
    public string RoomName { get; set; } = null!;
    public int Capacity { get; set; }
    public string Location { get; set; } = null!;
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}