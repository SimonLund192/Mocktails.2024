using Microsoft.AspNetCore.Mvc;
using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using Mocktails.WebApi.DTOs;
using Mocktails.WebApi.DTOs.Converters;

namespace Mocktails.WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderDAO _orderDAO;
    private readonly IMocktailDAO _mocktailDAO;

    public OrdersController(
        IOrderDAO orderDAO,
        IMocktailDAO mocktailDAO)
    {
        _orderDAO = orderDAO;
        _mocktailDAO = mocktailDAO;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetOrders(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var orders = await _orderDAO.GetOrdersAsync();
        var orderDTOs = orders.Select(OrderConverter.ToDTO).ToList();
        return orderDTOs;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var orderItems = new List<OrderItem>();

        foreach (var item in request.Products)
        {
            var mocktail = await _mocktailDAO.GetMocktailByIdAsync(item.Id);
            var orderItem = new OrderItem()
            {
                MocktailId = item.Id,
                Quantity = item.Quantity,
                Price = mocktail.Price
            };
            orderItems.Add(orderItem);
        }

        var order = new Order()
        {
            UserId = request.UserId,
            OrderDate = DateTime.Now,
            ShippingAddress = request.ShippingAddress,
            OrderItems = orderItems
        };

        var orderId = await _orderDAO.CreateOrderAsync(order);

        //return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = orderId }, null);

        return Ok(orderId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrderByIdAsync([FromRoute] int id)
    {
        var order = await _orderDAO.GetOrderByIdAsync(id);
        if (order is null)
            return NotFound();

        var orderDTO = OrderConverter.ToDTO(order);

        return Ok(orderDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] OrderDTO orderDTO)
    {
        if (id != orderDTO.Id)
        {
            ModelState.AddModelError(nameof(id), "Id's must match.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var order = OrderConverter.ToModel(orderDTO);
        order.Id = id;

        var success = await _orderDAO.UpdateOrderAsync(order);
        if (!success) return NotFound();

        return NoContent();
    }
}
