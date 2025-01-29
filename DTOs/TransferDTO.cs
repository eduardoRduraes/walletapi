namespace WalletAPI.DTOs;

public class TransferDTO
{
    public Guid ReceivedUserId { get; set; }
    public decimal Amount { get; set; }
}