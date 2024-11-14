using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.News;
using MRC_API.Service.Interface;
using Repository.Entity;
using AutoMapper;

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
    }
}