using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Threads.BusinessLogicLayer.DTO;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.BusinessLogicLayer.Services;

namespace Threads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<ActionResult> PostPost(PostDTO postDTO)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            postDTO.AuthorId = Guid.Parse(userId!);

            var postFromDb = await _postService.AddPost(postDTO);

            return CreatedAtAction(userId, postFromDb);
        }
    }
}
