namespace Mocktails.WebApi.DTOs;

public class PurchaseDTO
{
    public int MocktailId { get; set; }
    public int Quantity { get; set; }
    public byte[] RowVersion { get; set; }
}
