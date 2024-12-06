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
    private readonly IOrderItemDAO _orderItemDAO;

    public OrdersController(IOrderDAO orderDAO, IOrderItemDAO orderItemDAO)
    {
        _orderDAO = orderDAO;
        _orderItemDAO = orderItemDAO;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            var orders = await _orderDAO.GetOrdersAsync();
            var orderDTOs = orders.Select(OrderConverter.ToDTO).ToList();
            return Ok(orderDTOs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDTO)
    {
        try
        {
            var order = OrderConverter.ToModel(orderDTO);
            var orderId = await _orderDAO.CreateOrderAsync(order);

            return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = orderId }, null);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrderByIdAsync(int id)
    {
        try
        {
            // Fetch the order
            var order = await _orderDAO.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            // Fetch detailed order items
            var orderItems = await _orderItemDAO.GetOrderItemsByOrderIdAsync(id);

            // Convert the order and its items to DTOs
            var orderDTO = OrderConverter.ToDTO(order);
            orderDTO.OrderItems = orderItems.Select(OrderItemConverter.ToDTO).ToList();

            return Ok(orderDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("OrderItemsFromOrderId")]
    public async Task<IActionResult> GetOrderItemsByOrderIdAsync([FromQuery] int orderId)
    {
        if (orderId <= 0)
        {
            return BadRequest("Invalid OrderId. It must be greater than zero.");
        }

        try
        {
            var orderItems = await _orderItemDAO.GetOrderItemsByOrderIdAsync(orderId);
            if (orderItems == null || !orderItems.Any())
            {
                return NotFound($"No OrderItems found for OrderId {orderId}.");
            }

            var orderItemDTOs = orderItems.Select(OrderItemConverter.ToDTO).ToList();
            return Ok(orderItemDTOs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
