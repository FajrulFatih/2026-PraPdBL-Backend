using Microsoft.EntityFrameworkCore;
using PraPdBL_Backend.Data;
using PraPdBL_Backend.DTOs;
using PraPdBL_Backend.Models;
using PraPdBL_Backend.Services.Interfaces;

namespace PraPdBL_Backend.Services.Implementation;

public class RoomService : IRoomService
{
    private readonly AppDbContext _db;

    public RoomService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<(Room? room, bool conflict)> CreateAsync(RoomCreateDto dto)
    {
        var roomCode = dto.RoomCode.Trim();
        var roomName = dto.RoomName.Trim();
        var location = dto.Location.Trim();

        var exists = await _db.Rooms
            .AsNoTracking()
            .AnyAsync(r => r.DeletedAt == null && r.RoomCode == roomCode);
        if (exists) return (null, true);

        var room = new Room
        {
            RoomCode = roomCode,
            RoomName = roomName,
            Capacity = dto.Capacity,
            Location = location,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Rooms.Add(room);
        await _db.SaveChangesAsync();
        return (room, false);
    }

    public async Task<(int total, List<Room> data)> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var query = _db.Rooms
            .AsNoTracking()
            .Where(r => r.DeletedAt == null)
            .OrderBy(r => r.RoomName);

        var total = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (total, data);
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _db.Rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id && r.DeletedAt == null);
    }

    public async Task<(Room? room, bool conflict)> UpdateAsync(int id, RoomCreateDto dto)
    {
        var room = await _db.Rooms.FirstOrDefaultAsync(r => r.Id == id && r.DeletedAt == null);
        if (room == null) return (null, false);

        var roomCode = dto.RoomCode.Trim();
        var roomName = dto.RoomName.Trim();
        var location = dto.Location.Trim();

        var exists = await _db.Rooms
            .AsNoTracking()
            .AnyAsync(r => r.DeletedAt == null && r.RoomCode == roomCode && r.Id != id);
        if (exists) return (null, true);

        room.RoomCode = roomCode;
        room.RoomName = roomName;
        room.Capacity = dto.Capacity;
        room.Location = location;
        room.IsActive = dto.IsActive;
        room.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return (room, false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var room = await _db.Rooms.FirstOrDefaultAsync(r => r.Id == id && r.DeletedAt == null);
        if (room == null) return false;

        room.DeletedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<(bool restored, bool conflict)> RestoreAsync(int id)
    {
        var room = await _db.Rooms.FirstOrDefaultAsync(r => r.Id == id && r.DeletedAt != null);
        if (room == null) return (false, false);

        var exists = await _db.Rooms
            .AsNoTracking()
            .AnyAsync(r => r.DeletedAt == null && r.RoomCode == room.RoomCode && r.Id != id);
        if (exists) return (false, true);

        room.DeletedAt = null;
        room.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return (true, false);
    }
}
