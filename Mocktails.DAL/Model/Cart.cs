using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.DAL.Model;
public class Cart
{

    public Dictionary<int, MocktailQuantity> MocktailQuantities { get; set; }

}
