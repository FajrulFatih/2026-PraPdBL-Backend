namespace PraPdBL_Backend.DTOs;

public class BookingStatusHistoryDto
{
    public int Id { get; set; }
    public int BookingId { get; set; }

    public int OldStatusId { get; set; }
    public string? OldStatusLabel { get; set; }

    public int NewStatusId { get; set; }
    public string? NewStatusLabel { get; set; }

    public int ChangedById { get; set; }
    public string? ChangedByName { get; set; }

    public DateTime ChangedAt { get; set; }
    public string? Note { get; set; }

    public BookingHistoryRoomDto Room { get; set; } = new();
    public BookingHistoryUserDto User { get; set; } = new();
}

public class BookingHistoryRoomDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class BookingHistoryUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
