
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Response.Product;
using MRC_API.Service.Interface;
using Repository.Paginate;

namespace MRC_API.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService) : base(logger)
        {
            _productService = productService;
        }
        [HttpPost(ApiEndPointConstant.Product.CreateNewProduct)]
        [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest createProductRequest)
        {
           CreateProductResponse createProductResponse = await _productService.CreateProduct(createProductRequest);
            if(createProductResponse == null)
            {
                return Problem(MessageConstant.ProductMessage.CreateProductFail);
            }
            return CreatedAtAction(nameof(CreateProduct), createProductResponse);
        }

        [HttpGet(ApiEndPointConstant.Product.GetListProducts)]
        [ProducesResponseType(typeof(IPaginate<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetListProduct([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _productService.GetListProduct(pageNumber, pageSize);
            if (response == null)
            {
                return Problem(MessageConstant.ProductMessage.ProductIsEmpty);
            }
            return Ok(response);
        }
    }
}
