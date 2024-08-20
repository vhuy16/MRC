
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Response.Category;
using MRC_API.Payload.Response.User;
using MRC_API.Service.Interface;
using Repository.Paginate;

namespace MRC_API.Controllers
{
    public class CategoryController : BaseController<CategoryController>
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService) : base(logger)
        {
            _categoryService = categoryService;
        }

        [HttpPost(ApiEndPointConstant.Category.CreateNewCategory)]
        [ProducesResponseType(typeof(CreateNewCategoryResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewCategory([FromBody] CreateNewCategoryRequest createNewCategoryRequest)
        {
            CreateNewCategoryResponse createNewCategoryResponse = await _categoryService.CreateNewCategory(createNewCategoryRequest);
            if (createNewCategoryResponse == null)
            {
                return Problem(MessageConstant.CategoryMessage.CreateCategoryFail);
            }
            return CreatedAtAction(nameof(CreateNewCategory), createNewCategoryResponse);
        }
        [HttpGet(ApiEndPointConstant.Category.GetAllCategory)]
        [ProducesResponseType(typeof(IPaginate<GetCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllCategory([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _categoryService.GetAllCategory(pageNumber, pageSize);
            if (response == null)
            {
                return Problem(MessageConstant.CategoryMessage.CategoryIsEmpty);
            }
            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.Category.GetCategory)]
        [ProducesResponseType(typeof(GetCategoryResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            var response = await _categoryService.GetCategory(id);
            if (response == null)
            {
                return Problem(MessageConstant.CategoryMessage.CategoryIsEmpty);
            }
            return Ok(response);
        }

        [HttpPut(ApiEndPointConstant.Category.UpdateCategory)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            var response = await _categoryService.UpdateCategory(id, updateCategoryRequest);
            return Ok(response);
        }

        [HttpDelete(ApiEndPointConstant.Category.DeleteCategory)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var response = await _categoryService.DeleteCategory(id);
            return Ok(response);
        }
    }
}
