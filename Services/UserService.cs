using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using SalesTest.Entities;

public class UserService
{
    private readonly SaleDbContext _context;

    public UserService(SaleDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();

    public async Task<User?> GetUserByIdAsync(Guid id) => await _context.Users.FindAsync(id);

    public async Task<User?> GetUserByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User> CreateUserAsync(User user)
    {
        user.PasswordHash = HashPassword(user.PasswordHash);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(Guid id, User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.Email = updatedUser.Email;
        user.Username = updatedUser.Username;
        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        user.Phone = updatedUser.Phone;
        user.Status = updatedUser.Status;
        user.Role = updatedUser.Role;

        if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
            user.PasswordHash = HashPassword(updatedUser.PasswordHash);

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
