using PraPdBL_Backend.DTOs;
using PraPdBL_Backend.Models;

namespace PraPdBL_Backend.Services.Interfaces;

public interface IUserService
{
    Task<(User? user, bool conflict)> CreateAsync(UserCreateDto dto);
    Task<(int total, List<User> data)> GetAllAsync(int page = 1, int pageSize = 10);
    Task<User?> GetByIdAsync(int id);
    Task<(User? user, bool conflict)> UpdateAsync(int id, UserCreateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<(bool restored, bool conflict)> RestoreAsync(int id);
}
