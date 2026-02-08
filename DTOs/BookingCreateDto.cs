namespace PraPdBL_Backend;

public class BookingCreateDto
{
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
