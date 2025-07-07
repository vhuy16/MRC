
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Infrastructure;
using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response;
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
        [CustomAuthorize(roles: "Admin,Manager")]
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
                return CreatedAtAction(nameof(CreateProduct), new { id = response.data }, response);
            }
            else
            {
                return StatusCode(int.Parse(response.status), response);
            }
        }

        [HttpPost(ApiEndPointConstant.Product.UploadImg)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UploadImg(IFormFile formFile)
        {

            var response = await _productService.UpImageForDescription(formFile);

            return StatusCode(int.Parse(response.status), response);

        }
        [HttpGet(ApiEndPointConstant.Product.GetListProducts)]
        [ProducesResponseType(typeof(IPaginate<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetListProduct(
            [FromQuery] int? page,
            [FromQuery] int? size,
            [FromQuery] string? search = null, // Tìm kiếm tổng quát
            [FromQuery] bool? isAscending = null,
            [FromQuery] string? subCategoryName = null, // Lọc theo danh mục
            [FromQuery] decimal? minPrice = null, // Giá tối thiểu
            [FromQuery] decimal? maxPrice = null // Giá tối đa
        )
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;

            var response = await _productService.GetListProduct(
                pageNumber,
                pageSize,
                search,
                isAscending,
                subCategoryName,
                minPrice,
                maxPrice
            );

            if (response == null || response.data == null )
            {
                return Problem(detail: MessageConstant.ProductMessage.ProductIsEmpty, statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(response);
        }

        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpGet(ApiEndPointConstant.Product.GetAllProducts)]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllProduct([FromQuery] int? page,
                                                 [FromQuery] int? size,
                                                 [FromQuery] string? status,
                                                 [FromQuery] string searchName = null,
                                                 [FromQuery] bool? isAscending = null,
                                                 [FromQuery] string? subCategoryName = null)
        {

            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            
            var response = await _productService.GetAllProduct(pageNumber, pageSize, status, searchName, isAscending, subCategoryName);

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
        [CustomAuthorize(roles: "Admin,Manager")]
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
        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpPut(ApiEndPointConstant.Product.PatchProductImages)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> PatchProductImages([FromRoute] Guid id, [FromForm] List<IFormFile> newImages)
        {
            if (newImages == null)
            {
                return BadRequest("Image list cannot be null.");
            }

            var response = await _productService.PatchProductImages(id, newImages);

            if (response.status == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }

            return StatusCode(int.Parse(response.status), response);
        }
        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpDelete(ApiEndPointConstant.Product.DeleteProduct)] // Lưu ý: Có thể cần kiểm tra nếu endpoint này đúng cho Delete
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var response = await _productService.DeleteProduct(id);
    
            if (response.status == StatusCodes.Status404NotFound.ToString())
            {
                return NotFound(response);
            }
            if (response.status == StatusCodes.Status500InternalServerError.ToString())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }
        [HttpPut(ApiEndPointConstant.Product.EnableProduct)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> EnableProduct([FromRoute] Guid id)
        {
           

            var response = await _productService.EnableProduct(id);

            if (response.status == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }

            return StatusCode(int.Parse(response.status), response);
        }
        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpPut(ApiEndPointConstant.Product.DisableProduct)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DisableProduct([FromRoute] Guid id)
        {
            var response = await _productService.DisableProduct(id);

            if (response.status == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }

            return StatusCode(int.Parse(response.status), response);
        }
    }
}
