using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public class OrderDAO : IOrderDAO
{
    public Task<int> CreateOrderAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOrderAsync(Order entity)
    {
        throw new NotImplementedException();
    }
}
