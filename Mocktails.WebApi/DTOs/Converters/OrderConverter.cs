using Mocktails.DAL.Model;
using Mocktails.WebApi.Converters;
using Mocktails.WebApi.DTOs;

namespace Mocktails.WebApi.DTOs.Converters;

public static class OrderConverter
{

    public static OrderDTO ToDTO(this Order orderToConvert)
    {
        if (orderToConvert == null)
            throw new ArgumentNullException(nameof(orderToConvert), "Order object cannot be null.");

        var orderDTO = new OrderDTO();
        orderToConvert.CopyPropertiesTo(orderDTO);
        return orderDTO;
    }

    public static Order ToModel(this OrderDTO orderDTOToConvert)
    {
        if (orderDTOToConvert == null)
            throw new ArgumentNullException(nameof(orderDTOToConvert), "MocktailDTO object cannot be null.");

        var order = new Order();
        orderDTOToConvert.CopyPropertiesTo(order);
        return order;
    }

    public static IEnumerable<OrderDTO> ToDtos(this IEnumerable<Order> ordersToConvert)
    {
        if (ordersToConvert == null)
            throw new ArgumentNullException(nameof(ordersToConvert), "Orders collection cannot be null.");

        return ordersToConvert.Select(order => order.ToDTO());
    }

    public static IEnumerable<Order> ToModels(this IEnumerable<OrderDTO> orderDtosToConvert)
    {
        if (orderDtosToConvert == null)
            throw new ArgumentNullException(nameof(orderDtosToConvert), "OrderDTOs collection cannot be null.");

        return orderDtosToConvert.Select(orderDTO => orderDTO.ToModel());
    }
}
