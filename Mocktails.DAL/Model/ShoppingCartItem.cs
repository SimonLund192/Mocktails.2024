using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.DAL.Model;
public class ShoppingCartItem
{
    public int Id { get; set; }
    public int MocktailId { get; set; }
    public string MocktailName { get; set; }
    public string MocktailDescription { get; set; }
    public string MocktailImageUrl { get; set; }
    public int Quantity { get; set; }
    public decimal MocktailPrice { get; set; }
    public decimal TotalPrice { get; set; } // Add TotalPrice to store the calculated value
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
