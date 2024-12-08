﻿using Microsoft.AspNetCore.Mvc;
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

    public OrdersController(
        IOrderDAO orderDAO,
        IOrderItemDAO orderItemDAO)
    {
        _orderDAO = orderDAO;
        _orderItemDAO = orderItemDAO;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var orders = await _orderDAO.GetOrdersAsync();
        var orderDTOs = orders.Select(OrderConverter.ToDTO).ToList();
        return Ok(orderDTOs);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDTO)
    {
        var order = OrderConverter.ToModel(orderDTO);
        var orderId = await _orderDAO.CreateOrderAsync(order);

        return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = orderId }, null);
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
}
