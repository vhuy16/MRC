using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.News;
using MRC_API.Service.Interface;
using Repository.Entity;
using AutoMapper;
using MRC_API.Payload.Request.News;
using Repository.Enum;

namespace MRC_API.Controllers
{
    public class NewsController : BaseController<NewsController>
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public NewsController(ILogger<NewsController> logger, INewsService newsService, IMapper mapper) : base(logger)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        [HttpPost(ApiEndPointConstant.News.CreateFromExternal)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewsFromExternalSource([FromBody] string sourceUrl)
        {
            var response = await _newsService.CreateNewsFromExternalSource(sourceUrl);

            if (response.status == StatusCodes.Status400BadRequest.ToString())
            {
                return BadRequest(response);
            }

            return StatusCode(int.Parse(response.status), response);
        }

        [HttpPost(ApiEndPointConstant.News.CreateNews)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNews([FromBody] CreateNewsRequest request)
        {

            var response = await _newsService.CreateNews(request);

            return StatusCode(int.Parse(response.status), response);

        }

        [HttpGet(ApiEndPointConstant.News.GetAllNews)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllNews([FromQuery] int? page, [FromQuery] int? size, [FromQuery] TypeNewsEnum type, [FromQuery] Guid? ignoreId)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;

            var response = await _newsService.GetAllNews(pageNumber, pageSize, type, ignoreId);

            return StatusCode(int.Parse(response.status), response);

        }

        [HttpGet(ApiEndPointConstant.News.GetNewsById)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetNewsById([FromRoute] Guid id)
        {

            var response = await _newsService.GetNewsById(id);

            return StatusCode(int.Parse(response.status), response);

        }

        [HttpDelete(ApiEndPointConstant.News.DeleteNewsById)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteNewsById([FromRoute] Guid id)
        {

            var response = await _newsService.DeleteNews(id);

            return StatusCode(int.Parse(response.status), response);

        }
    }
}