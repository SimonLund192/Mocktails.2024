using Mocktails.ApiClient.Orders.DTOs;
using Mocktails.DAL.Model;

namespace Mocktails.WebApi.DTOs.Converters;

public static class OrderItemConverter
{
    /// <summary>
    /// Converts an OrderItem entity to an OrderItemDTO.
    /// </summary>
    /// <param name="orderItemToConvert">The OrderItem entity to convert.</param>
    /// <returns>The converted OrderItemDTO.</returns>
    public static OrderItemDTO ToDTO(this OrderItem orderItemToConvert)
    {
        if (orderItemToConvert == null)
            throw new ArgumentNullException(nameof(orderItemToConvert), "OrderItem object cannot be null.");

        var orderItemDTO = new OrderItemDTO();
        orderItemToConvert.CopyPropertiesTo(orderItemDTO);
        return orderItemDTO;
    }

    /// <summary>
    /// Converts an OrderItemDTO to an OrderItem entity.
    /// </summary>
    /// <param name="orderItemDTOToConvert">The OrderItemDTO to convert.</param>
    /// <returns>The converted OrderItem entity.</returns>
    public static OrderItem ToModel(this OrderItemDTO orderItemDTOToConvert)
    {
        if (orderItemDTOToConvert == null)
            throw new ArgumentNullException(nameof(orderItemDTOToConvert), "OrderItemDTO cannot be null.");

        var orderItem = new OrderItem();
        orderItemDTOToConvert.CopyPropertiesTo(orderItem);
        return orderItem;
    }

    /// <summary>
    /// Converts a collection of OrderItem entities to a collection of OrderItemDTOs.
    /// </summary>
    /// <param name="orderItemsToConvert">The collection of OrderItem entities to convert.</param>
    /// <returns>A collection of OrderItemDTOs.</returns>
    public static IEnumerable<OrderItemDTO> ToDtos(this IEnumerable<OrderItem> orderItemsToConvert)
    {
        if (orderItemsToConvert == null)
            throw new ArgumentNullException(nameof(orderItemsToConvert), "OrderItem collection cannot be null.");

        return orderItemsToConvert.Select(orderItem => orderItem.ToDTO());
    }

    /// <summary>
    /// Converts a collection of OrderItemDTOs to a collection of OrderItem entities.
    /// </summary>
    /// <param name="orderItemDtosToConvert">The collection of OrderItemDTOs to convert.</param>
    /// <returns>A collection of OrderItem entities.</returns>
    public static IEnumerable<OrderItem> ToModels(this IEnumerable<OrderItemDTO> orderItemDtosToConvert)
    {
        if (orderItemDtosToConvert == null)
            throw new ArgumentNullException(nameof(orderItemDtosToConvert), "OrderItemDTO collection cannot be null.");

        return orderItemDtosToConvert.Select(orderItemDTO => orderItemDTO.ToModel());
    }
}
