using PraPdBL_Backend.Data;
using PraPdBL_Backend.Models;

public static class DbInitializer
{
    public static void SeedAdmin(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.Users.Any(u => u.Email == "admin@kampus.ac.id"))
        {
            context.Users.Add(new User
            {
                Name = "Super Admin",
                Email = "admin@kampus.ac.id",
                Role = "ADMIN",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            context.SaveChanges();
        }
    }
}
