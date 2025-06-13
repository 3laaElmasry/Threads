using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Threads.BusinessLogicLayer.DTO.PostDTO;
using Threads.BusinessLogicLayer.DTO.PostExtenstions;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.BusinessLogicLayer.Services;

namespace Threads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostCreate(PostRequest postDTO)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            postDTO.AuthorId = "ad894473-33e1-4129-2eb7-08ddaa41f013";

            var postFromDb = await _postService.AddPost(postDTO);
            return CreatedAtAction(nameof(GetPostById), new { id = postFromDb.PostId }, postFromDb);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostResponse>> GetPostById(string id)
        {
            var post = await _postService.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }



    }
}
