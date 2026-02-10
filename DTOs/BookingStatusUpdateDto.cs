using System.ComponentModel.DataAnnotations;

namespace PraPdBL_Backend.DTOs;

public class BookingStatusUpdateDto
{
    [Range(1, int.MaxValue)]
    public int StatusId { get; set; }

    [Range(1, int.MaxValue)]
    public int ChangedBy { get; set; }

    [MaxLength(255)]
    public string? Note { get; set; }
}
