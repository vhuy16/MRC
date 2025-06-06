
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Infrastructure;
using MRC_API.Payload.Request.SubCategory;
using MRC_API.Payload.Response;
using MRC_API.Service.Interface;

namespace MRC_API.Controllers
{
    public class SubCategoryController : BaseController<SubCategoryController>
    {
        private readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(ILogger<SubCategoryController> logger, ISubCategoryService subCategoryService) : base(logger)
        {
            _subCategoryService = subCategoryService;
        }

        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpPost(ApiEndPointConstant.SubCategory.CreateSubCategory)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewSubCategory([FromBody] CreateSubCategoryRequest createSubCategoryRequest)
        {
            var response = await _subCategoryService.CreateSubCategory(createSubCategoryRequest);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpGet(ApiEndPointConstant.SubCategory.GetSubCategories)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetSubCategory([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _subCategoryService.GetSubCategories(pageNumber, pageSize);
            return StatusCode(int.Parse(response.status), response);
        }
        
        [HttpGet(ApiEndPointConstant.SubCategory.GetSubCategory)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetSubCategory([FromRoute] Guid id)
        {
            var response = await _subCategoryService.GetSubCategory(id);
            return StatusCode(int.Parse(response.status), response);
        }

        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpDelete(ApiEndPointConstant.SubCategory.DeleteSubCategory)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] Guid id)
        {
            var response = await _subCategoryService.DeleteSubCategory(id);
            return StatusCode(int.Parse(response.status), response);
        }

        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpPut(ApiEndPointConstant.SubCategory.UpdateSubCategory)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateSubCategory([FromRoute] Guid id, [FromBody] UpdateSubCategoryRequest request)
        {
            var response = await _subCategoryService.UpdateSubCategory(id, request);
            return StatusCode(int.Parse(response.status), response);
        }
        
        [HttpGet(ApiEndPointConstant.SubCategory.GetListSubCategoryByCategoryId)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetListsubCategoryByCategoryId([FromRoute] Guid id, [FromQuery] int? page, [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _subCategoryService.GetListSubCategoryByCategoryId(id, pageNumber, pageSize);
            return StatusCode(int.Parse(response.status), response);
        }
    }
}
