﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public interface IOrderItemDAO
{
    Task<IEnumerable<OrderItem>> GetOrderItemsAsync();
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int id);
    Task<OrderItem> GetOrderItemByIdAsync(int id);
    Task<int> CreateOrderItemAsync(OrderItem entity);
    Task<bool> UpdateOrderItemAsync(OrderItem entity);
    Task<bool> DeleteOrderItemAsync(int id);
}
