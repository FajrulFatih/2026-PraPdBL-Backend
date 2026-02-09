using PraPdBL_Backend.Models;
using PraPdBL_Backend.DTOs;

namespace PraPdBL_Backend.Services.Interfaces;

public interface IBookingService
{
    Task<Booking> CreateAsync(BookingCreateDto dto);
    Task<(int total, List<Booking> data)> GetAllAsync(int page = 1, int pageSize = 10);
    Task<Booking?> GetByIdAsync(int id);
    Task<Booking?> UpdateAsync(int id, BookingCreateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> HasConflictAsync(int roomId, DateTime start, DateTime end, int? excludeId = null);
}
