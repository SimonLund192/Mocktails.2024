using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.DAL.Model;
public class ShoppingCartItem
{
    public int Id { get; set; }
    public string SessionId { get; set; }
    public int MocktailId { get; set; }
    public int Quantity { get; set; }
    public string MocktailName { get; set; } // Add this
    public decimal MocktailPrice { get; set; } // Add this
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
