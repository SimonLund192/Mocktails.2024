using System.ComponentModel.DataAnnotations;

namespace Mocktails.ApiClient.Products.DTOs;

public class MocktailDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
    public byte[] RowVersion { get; set; } // Add RowVersion
}

