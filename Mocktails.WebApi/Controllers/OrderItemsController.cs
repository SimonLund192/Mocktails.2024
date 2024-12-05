using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Orders.DTOs;
using Mocktails.DAL.DaoClasses;
using Mocktails.WebApi.DTOs.Converters;

namespace Mocktails.WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class OrderItemsController : Controller
{
    private readonly IOrderItemDAO _orderItemDAO;
    private readonly IOrderDAO _orderDAO;

    public OrderItemsController(IOrderItemDAO orderItemDAO)
    {
        _orderItemDAO = orderItemDAO;
    }
    [HttpGet("Get all OrderItems")]
    public async Task<IActionResult> GetOrderItems()
    {
        var orderItems = await _orderItemDAO.GetOrderItemsAsync();
        return Ok(orderItems);
    }

    [HttpGet("OrderItemsFromOrderId")]
    public async Task<IActionResult> GetOrderItemsFromOrderByOrderIdAsync([FromQuery] int orderId)
    {
        if (orderId <= 0)
        {
            return BadRequest("Invalid OrderId. It must be greater than zero.");
        }

        var orderItems = await _orderItemDAO.GetOrderItemsByOrderIdAsync(orderId); // Call the DAO method
        if (orderItems == null || !orderItems.Any())
        {
            return NotFound($"No OrderItems found for OrderId {orderId}.");
        }

        return Ok(orderItems);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemDTO orderItemDTO)
    {
        var orderItem = OrderItemConverter.ToModel(orderItemDTO);
        var orderItemId = await _orderItemDAO.CreateOrderItemAsync(orderItem);

        return Created();
    }

    [HttpPut("Update OrderItem")]
    public async Task<IActionResult> UpdateOrderItemAsync([FromRoute]int id, [FromBody] OrderItemDTO orderItemDTO)
    {
        if (id != orderItemDTO.Id)
        {
            ModelState.AddModelError(nameof(id), "Id's must match");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var orderItem = OrderItemConverter.ToModel(orderItemDTO);
        orderItem.Id = id;

        var success = await _orderItemDAO.UpdateOrderItemAsync(orderItem);
        if (!success) return NotFound();

        return NoContent();
    }
}
