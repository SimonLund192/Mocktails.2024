//using Microsoft.AspNetCore.Mvc;
//using Mocktails.DAL.DaoClasses;
//using Mocktails.DAL.Model;
//using Mocktails.WebApi.DTOs;

//namespace Mocktails.WebApi.Controllers
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class ShoppingCartController : ControllerBase
//    {
//        private readonly IShoppingCartDAO _cartDAO;
//        private readonly IOrderDAO _orderDAO;

//        public ShoppingCartController(IShoppingCartDAO cartDAO, IOrderDAO orderDAO)
//        {
//            _cartDAO = cartDAO;
//            _orderDAO = orderDAO;
//        }

//        [HttpGet("Get all CartItems")]
//        public async Task<IActionResult> GetCartItemsWithDetails()
//        {
//            try
//            {
//                var cartItems = await _cartDAO.GetCartItemsWithDetailsAsync();

//                if (!cartItems.Any())
//                    return NotFound("No items found in the shopping cart.");

//                return Ok(cartItems);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddToCart([FromBody] ShoppingCartItem item)
//        {
//            if (item == null || item.MocktailId <= 0 || item.Quantity <= 0)
//            {
//                return BadRequest("Invalid cart item. Ensure MocktailId and Quantity are valid.");
//            }

//            try
//            {
//                var itemId = await _cartDAO.AddToCartAsync(item);
//                return Ok(new { Id = itemId });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Error adding item to cart: {ex.Message}");
//            }
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] ShoppingCartItem item)
//        {
//            item.Id = id;
//            var success = await _cartDAO.UpdateCartItemAsync(item);
//            if (!success) return NotFound("Cart item not found.");
//            return Ok("Cart item updated successfully.");
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> RemoveFromCart(int id)
//        {
//            var success = await _cartDAO.RemoveFromCartAsync(id);
//            if (!success) return NotFound("Cart item not found.");
//            return Ok("Cart item removed successfully.");
//        }

//        [HttpPost("checkout")]
//        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
//        {
//            if (request == null || string.IsNullOrWhiteSpace(request.ShippingAddress))
//            {
//                return BadRequest("Invalid checkout request. Shipping address is required.");
//            }

//            try
//            {
//                // Get the shopping cart items
//                var cartItems = await _cartDAO.GetCartItemsWithDetailsAsync();
//                if (!cartItems.Any())
//                {
//                    return BadRequest("Shopping cart is empty.");
//                }

//                // Create the order
//                var orderId = await _orderDAO.CreateOrderFromCartAsync(request.UserId, cartItems, request.ShippingAddress);

//                // Clear the shopping cart
//                await _cartDAO.ClearCartAsync();

//                return Ok(new { OrderId = orderId });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }

//        [HttpDelete("clear")]
//        public async Task<IActionResult> ClearCart()
//        {
//            var success = await _cartDAO.ClearCartAsync();
//            if (!success)
//                return BadRequest("Failed to clear the shopping cart.");

//            return Ok("Shopping cart cleared successfully.");
//        }
//    }
//}
