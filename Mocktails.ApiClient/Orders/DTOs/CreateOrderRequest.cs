using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.ApiClient.Orders.DTOs;
public class CreateOrderRequest
{
    [Required]
    public string ShippingAddress { get; set; } = "";

    public List<Product> Products { get; set; } = [];

    public class Product
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
