using WalletAPI.Data;
using WalletAPI.Models;

namespace WalletAPI.Services;

public class WalletServices
{
    private readonly AppDbContext _context;
    
    public WalletServices(AppDbContext context)
    {
        _context = context;
    }

    public decimal GetBalance(Guid userId)
    {
        var wallet = _context.Wallets.FirstOrDefault(w => w.UserId == userId);
        if(wallet == null)
                throw new Exception("Wallet not found");
        
        return wallet.Balance;
    }

    public void AddBalance(Guid userId, decimal amount)
    {
        if(amount < 0)
            throw new Exception("Invalid amount");
        
        var wallet = _context.Wallets.FirstOrDefault(w => w.UserId == userId);
        if(wallet == null)
            throw new Exception("Wallet not found");
        
        wallet.Balance += amount;
        _context.SaveChanges();
    }
}