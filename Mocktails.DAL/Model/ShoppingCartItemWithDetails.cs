using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.DAL.Model;
public class ShoppingCartItemWithDetails
{
    public int CartItemId { get; set; }
    public string SessionId { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int MocktailId { get; set; }
    public string MocktailName { get; set; }
    public string MocktailDescription { get; set; }
    public decimal MocktailPrice { get; set; }
    public string MocktailImageUrl { get; set; }
}
