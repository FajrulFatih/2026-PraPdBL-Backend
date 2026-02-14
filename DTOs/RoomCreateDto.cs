using System.ComponentModel.DataAnnotations;

namespace PraPdBL_Backend.DTOs;

public class RoomCreateDto
{
    [Required]
    [MaxLength(50)]
    public string RoomCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string RoomName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    [Required]
    [MaxLength(120)]
    public string Location { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
