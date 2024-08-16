
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Response.Category;
using MRC_API.Payload.Response.User;
using MRC_API.Service.Interface;

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
    }
}
