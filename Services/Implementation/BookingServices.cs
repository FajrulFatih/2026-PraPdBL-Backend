using Microsoft.EntityFrameworkCore;
using PraPdBL_Backend.Data;
using PraPdBL_Backend.Models;
using PraPdBL_Backend.DTOs;
using PraPdBL_Backend.Services.Interfaces;

namespace PraPdBL_Backend.Services.Implementation;

public class BookingService : IBookingService
{
    private readonly AppDbContext _db;

    public BookingService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Booking> CreateAsync(BookingCreateDto dto)
    {
        var booking = new Booking
        {
            RoomId = dto.RoomId,
            UserId = dto.UserId,
            Purpose = dto.Purpose,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            StatusId = 1, // pending
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();
        return booking;
    }

    public async Task<(int total, List<Booking> data)> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var query = _db.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .Include(b => b.Status)
            .Where(b => b.DeletedAt == null)
            .OrderByDescending(b => b.StartTime);

        var total = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (total, data);
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _db.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .Include(b => b.Status)
            .FirstOrDefaultAsync(b => b.Id == id && b.DeletedAt == null);
    }

    public async Task<Booking?> UpdateAsync(int id, BookingCreateDto dto)
    {
        var booking = await _db.Bookings.FindAsync(id);
        if (booking == null || booking.DeletedAt != null) return null;

        booking.RoomId = dto.RoomId;
        booking.UserId = dto.UserId;
        booking.Purpose = dto.Purpose;
        booking.StartTime = dto.StartTime;
        booking.EndTime = dto.EndTime;
        booking.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return booking;
    }

    public async Task<(Booking? booking, bool statusNotFound)> UpdateStatusAsync(int id, BookingStatusUpdateDto dto)
    {
        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id && b.DeletedAt == null);
        if (booking == null) return (null, false);

        var statusExists = await _db.BookingStatuses.AnyAsync(s => s.Id == dto.StatusId);
        if (!statusExists) return (null, true);

        var oldStatus = booking.StatusId;
        if (oldStatus != dto.StatusId)
        {
            booking.StatusId = dto.StatusId;
            booking.UpdatedAt = DateTime.UtcNow;

            var history = new BookingStatusHistory
            {
                BookingId = booking.Id,
                OldStatus = oldStatus,
                NewStatus = dto.StatusId,
                ChangedBy = dto.ChangedBy,
                ChangedAt = DateTime.UtcNow,
                Note = string.IsNullOrWhiteSpace(dto.Note) ? null : dto.Note.Trim()
            };

            _db.BookingStatusHistories.Add(history);
        }
        else
        {
            booking.UpdatedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();

        var updated = await _db.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .Include(b => b.Status)
            .FirstOrDefaultAsync(b => b.Id == id && b.DeletedAt == null);

        return (updated, false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var booking = await _db.Bookings.FindAsync(id);
        if (booking == null || booking.DeletedAt != null) return false;

        booking.DeletedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HasConflictAsync(int roomId, DateTime start, DateTime end, int? excludeId = null)
    {
        return await _db.Bookings.AnyAsync(b =>
            b.RoomId == roomId &&
            b.DeletedAt == null &&
            b.StartTime < end &&
            b.EndTime > start &&
            (!excludeId.HasValue || b.Id != excludeId.Value));
    }
}
