using System.ComponentModel.DataAnnotations;

namespace Mocktails.WebApi.DTOs;

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
}
