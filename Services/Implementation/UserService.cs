using Microsoft.EntityFrameworkCore;
using PraPdBL_Backend.Data;
using PraPdBL_Backend.DTOs;
using PraPdBL_Backend.Models;
using PraPdBL_Backend.Services.Interfaces;

namespace PraPdBL_Backend.Services.Implementation;

public class UserService : IUserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<(User? user, bool conflict)> CreateAsync(UserCreateDto dto)
    {
        var email = dto.Email.Trim();
        var name = dto.Name.Trim();
        var role = string.IsNullOrWhiteSpace(dto.Role) ? "USER" : dto.Role.Trim().ToUpperInvariant();

        var exists = await _db.Users
            .AsNoTracking()
            .AnyAsync(u => u.DeletedAt == null && u.Email == email);
        if (exists) return (null, true);

        var user = new User
        {
            Name = name,
            Email = email,
            Role = role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return (user, false);
    }

    public async Task<(int total, List<User> data)> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var query = _db.Users
            .AsNoTracking()
            .Where(u => u.DeletedAt == null)
            .OrderBy(u => u.Name);

        var total = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (total, data);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
    }

    public async Task<(User? user, bool conflict)> UpdateAsync(int id, UserCreateDto dto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
        if (user == null) return (null, false);

        var email = dto.Email.Trim();
        var name = dto.Name.Trim();
        var role = string.IsNullOrWhiteSpace(dto.Role) ? "USER" : dto.Role.Trim().ToUpperInvariant();

        var exists = await _db.Users
            .AsNoTracking()
            .AnyAsync(u => u.DeletedAt == null && u.Email == email && u.Id != id);
        if (exists) return (null, true);

        user.Name = name;
        user.Email = email;
        user.Role = role;
        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return (user, false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
        if (user == null) return false;

        user.DeletedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<(bool restored, bool conflict)> RestoreAsync(int id)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt != null);
        if (user == null) return (false, false);

        var exists = await _db.Users
            .AsNoTracking()
            .AnyAsync(u => u.DeletedAt == null && u.Email == user.Email && u.Id != id);
        if (exists) return (false, true);

        user.DeletedAt = null;
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return (true, false);
    }
}
