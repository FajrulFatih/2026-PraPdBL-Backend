namespace PraPdBL_Backend.DTOs;

public class RoomListItemDto
{
    public int Id { get; set; }
    public string RoomCode { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Location { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? BookingStatus { get; set; }
}
