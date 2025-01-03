using Mocktails.ApiClient.Orders.DTOs;
using Mocktails.DAL.Model;

namespace Mocktails.WebApi.DTOs.Converters;

public static class OrderConverter
{
    public static OrderDTO ToDTO(this Order orderToConvert)
    {
        ArgumentNullException.ThrowIfNull(orderToConvert);

        var orderDTO = new OrderDTO();
        orderToConvert.CopyPropertiesTo(orderDTO);

        orderDTO.OrderItems = orderToConvert.OrderItems.Select(o =>
        {
            var orderItemDTO = new OrderItemDTO();
            o.CopyPropertiesTo(orderItemDTO);
            return orderItemDTO;
        }).ToList();

        return orderDTO;
    }

    public static Order ToModel(this OrderDTO orderDTOToConvert)
    {
        ArgumentNullException.ThrowIfNull(orderDTOToConvert);

        var order = new Order();
        orderDTOToConvert.CopyPropertiesTo(order);
        return order;
    }

    public static IEnumerable<OrderDTO> ToDtos(this IEnumerable<Order> ordersToConvert)
    {
        ArgumentNullException.ThrowIfNull(ordersToConvert);

        return ordersToConvert.Select(order => order.ToDTO());
    }

    public static IEnumerable<Order> ToModels(this IEnumerable<OrderDTO> orderDtosToConvert)
    {
        ArgumentNullException.ThrowIfNull(orderDtosToConvert);

        return orderDtosToConvert.Select(orderDTO => orderDTO.ToModel());
    }
}
