
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Response.Cart;
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

        [HttpPost(ApiEndPointConstant.Cart.CreateNewCart)]
        [ProducesResponseType(typeof(CreateNewCartResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewCart()
        {
            CreateNewCartResponse createNewCartResponse = await _cartService.CreateCart();
            if (createNewCartResponse == null)
            {
                return Problem(MessageConstant.CategoryMessage.CreateCategoryFail);
            }
            return CreatedAtAction(nameof(CreateNewCart), createNewCartResponse);
        }

    }
}
