using Core.Entity;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoEcommerce.Controllers
{
 
    public class CartController(ICartService cartService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string cartId)
        {   var cart = await cartService.GetCartAsync(cartId);
            // Logic to get the cart by cartId
            return Ok(cart ??new ShoppingCart { Id=cartId });
        }
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            // Logic to set the cart
            var updatedCart = await cartService.SetCartAsync(cart);
            if (updatedCart == null)
            {
                return BadRequest("Failed to update the cart");
            }
            return Ok(updatedCart);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string cartId)
        {
            // Logic to delete the cart
            var result = await cartService.DeleteCartAsync(cartId);
            if (!result)
            {
                return BadRequest("Failed to delete the cart");
            }
            return Ok(result);
        }

    }

}
