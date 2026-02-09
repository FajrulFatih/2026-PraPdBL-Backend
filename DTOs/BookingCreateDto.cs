using System.ComponentModel.DataAnnotations;
using PraPdBL_Backend.Data;

namespace PraPdBL_Backend.DTOs;

public class BookingCreateDto : IValidatableObject
{
    [Range(1, int.MaxValue)]
    public int RoomId { get; set; }

    [Range(1, int.MaxValue)]
    public int UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Purpose { get; set; } = string.Empty;

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime harus setelah StartTime.",
                new[] { nameof(EndTime) });

            yield break;
        }

        var dbContext = validationContext.GetService(typeof(AppDbContext)) as AppDbContext;
        if (dbContext is null)
        {
            yield break;
        }

        var hasOverlap = dbContext.Bookings.Any(
            b => b.RoomId == RoomId
                && b.DeletedAt == null
                && b.StartTime < EndTime
                && b.EndTime > StartTime);

        if (hasOverlap)
        {
            yield return new ValidationResult(
                "Room sudah terbooking pada rentang waktu tersebut.",
                new[] { nameof(StartTime), nameof(EndTime) });
        }
    }
}
