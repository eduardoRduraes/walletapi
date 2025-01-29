using System.Security.Cryptography;
using System.Text;
using WalletAPI.Data;
using WalletAPI.Models;

namespace WalletAPI.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User AuthenticateUser(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);

        if (user == null || user.PasswordHash != HashPassword(password))
        {
            return null;
        }
        return user;
    }

    public User CreateUser(string username, string password)
    {
        if(_context.Users.Any(u => u.Username ==username))
            throw new Exception("Username already exists");

        var user = new User()
        {
            Username = username,
            PasswordHash = HashPassword(password)
        };
        
        _context.Users.Add(user);
        _context.SaveChanges();

        var wallet = new Wallet
        {
            UserId = user.Id,
            Balance = 0
        };
        
        _context.Wallets.Add(wallet);
        _context.SaveChanges();
        
        return user;
    }

    private string HashPassword(string password)
    {
        using var sh256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sh256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}