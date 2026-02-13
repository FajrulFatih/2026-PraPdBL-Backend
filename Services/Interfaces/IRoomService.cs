using PraPdBL_Backend.DTOs;
using PraPdBL_Backend.Models;

namespace PraPdBL_Backend.Services.Interfaces;

public interface IRoomService
{
    Task<(Room? room, bool conflict)> CreateAsync(RoomCreateDto dto);
    Task<(int total, List<RoomListItemDto> data)> GetAllAsync(int page = 1, int pageSize = 10);
    Task<Room?> GetByIdAsync(int id);
    Task<(Room? room, bool conflict)> UpdateAsync(int id, RoomCreateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<(bool restored, bool conflict)> RestoreAsync(int id);
}
