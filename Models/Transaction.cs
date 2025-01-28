namespace WalletAPI.Models;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SenderWalletId { get; set; }
    public Guid ReceiverWalletId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}