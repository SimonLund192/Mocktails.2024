using Microsoft.AspNetCore.Mvc;

using Mocktails.DAL.DaoClasses;
using Mocktails.WebApi.DTOs;
using Mocktails.WebApi.DTOs.Converters;

namespace Mocktails.WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderDAO _orderDAO;

    public OrdersController(IOrderDAO orderDAO)
    {
        _orderDAO = orderDAO;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderDAO.GetOrdersAsync();
        return Ok(orders);
    }

    // CreateOrderAsync
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDTO)
    {
        var order = OrderConverter.ToModel(orderDTO);
        var orderId = await _orderDAO.CreateOrderAsync(order);

        return Created();
    }

    // GetOrderByIdAsync

}
