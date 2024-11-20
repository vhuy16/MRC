
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response.Product;
using MRC_API.Service.Implement;
using MRC_API.Service.Interface;
using Repository.Paginate;
using System.Drawing.Printing;

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
            if (createProductRequest == null)
            {
                return BadRequest("Product request cannot be null.");
            }

            var response = await _productService.CreateProduct(createProductRequest);

            if (response.status == StatusCodes.Status201Created.ToString())
            {
                return CreatedAtAction(nameof(CreateProduct), new { id = response.data }, response.data);
            }
            else
            {
                return StatusCode(int.Parse(response.status), response);
            }
        }

        [HttpGet(ApiEndPointConstant.Product.GetListProducts)]
        [ProducesResponseType(typeof(IPaginate<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetListProduct(
                                                 [FromQuery] int? page,
                                                 [FromQuery] int? size,
                                                 [FromQuery] string searchName = null,
                                                 [FromQuery] bool? isAscending = null)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;

            var response = await _productService.GetListProduct(pageNumber, pageSize, searchName, isAscending);

            if (response == null || response.data == null)
            {
                return Problem(detail: MessageConstant.ProductMessage.ProductIsEmpty, statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(response);
        }
        [HttpGet(ApiEndPointConstant.Product.GetAllProducts)]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllProduct()
        {
            var response = await _productService.GetAllProduct();

            if (response == null || response.data == null)
            {
                return Problem(detail: MessageConstant.ProductMessage.ProductIsEmpty, statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.Product.GetListProductsByCategoryId)]
        [ProducesResponseType(typeof(IPaginate<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetListProductByCategoryId([FromRoute] Guid id, [FromQuery] int? page, [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _productService.GetListProductByCategoryId(id, pageNumber, pageSize);
            if (response == null)
            {
                return Problem(MessageConstant.ProductMessage.ProductIsEmpty);
            }
            return Ok(response);
        }
        [HttpGet(ApiEndPointConstant.Product.GetProductById)]
        [ProducesResponseType(typeof(IPaginate<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            
            var response = await _productService.GetProductById(id);
            if (response == null)
            {
                return Problem(MessageConstant.ProductMessage.ProductIsEmpty);
            }
            return Ok(response);
        }
        [HttpPut(ApiEndPointConstant.Product.UpdateProduct)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromForm] UpdateProductRequest updateProductRequest)
        {
            if (updateProductRequest == null)
            {
                return BadRequest("Product request cannot be null.");
            }

            var response = await _productService.UpdateProduct(id, updateProductRequest);

            if (response.status == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }

            return StatusCode(int.Parse(response.status), response);
        }
        [HttpDelete(ApiEndPointConstant.Product.UpdateProduct)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
