using Mocktails.ApiClient.Products.DTOs;

namespace Mocktails.Website.Models;

public static class MocktailQuantityConverter
{
    public static MocktailQuantity ToModel(this MocktailQuantityDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        return new MocktailQuantity
        {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity
        };
    }

    public static MocktailQuantityDTO ToDto(this MocktailQuantity model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        return new MocktailQuantityDTO
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Quantity = model.Quantity
        };
    }
}
