
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.CartItem;
using MRC_API.Payload.Response.Cart;
using MRC_API.Payload.Response.CartItem;
using MRC_API.Service.Implement;
using MRC_API.Service.Interface;

namespace MRC_API.Controllers
{
    public class CartController : BaseController<CartController>
    {
        private readonly ICartService _cartService;
        public CartController(ILogger<CartController> logger, ICartService cartService) : base(logger)
        {
            _cartService = cartService;
        }

        [HttpPost(ApiEndPointConstant.Cart.AddCartItem)]
        [ProducesResponseType(typeof(AddCartItemResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemRequest addCartItemRequest)
        {
            AddCartItemResponse addCartItemResponse = await _cartService.AddCartItem(addCartItemRequest);
            if (addCartItemResponse == null)
            {
                return Problem(MessageConstant.CartMessage.AddCartItemFail);
            }
            return CreatedAtAction(nameof(AddCartItem), addCartItemResponse);
        }

        [HttpDelete(ApiEndPointConstant.Cart.DeleteCartItem)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteCartItem([FromRoute] Guid itemId)
        {
            var response = await _cartService.DeleteCartItem(itemId);
            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.Cart.GetAllCart)]
        [ProducesResponseType(typeof(List<GetAllCartItemResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllCart()
        {
            var response = await _cartService.GetAllCartItem();
            if (response == null)
            {
                return Problem(MessageConstant.CartMessage.CartItemIsEmpty);
            }
            return Ok(response);
        }

        [HttpDelete(ApiEndPointConstant.Cart.ClearCart)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> ClearAllCart()
        {
            var response = await _cartService.ClearCart();
            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.Cart.GetCartSummary)]
        [ProducesResponseType(typeof(CartSummayResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetCartSummary()
        {
            var response = await _cartService.GetCartSummary();
            return Ok(response);
        }

        [HttpPut(ApiEndPointConstant.Cart.UpdateCartItem)]
        [ProducesResponseType(typeof(UpdateCartItemResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateCartItem([FromRoute] Guid itemId, [FromBody] UpdateCartItemRequest updateCartItemRequest)
        {
            var response = await _cartService.UpdateCartItem(itemId, updateCartItemRequest);
            return Ok(response);
        }
    }
}
