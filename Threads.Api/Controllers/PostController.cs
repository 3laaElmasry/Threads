using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Threads.BusinessLogicLayer.DTO.PostDTO;
using Threads.BusinessLogicLayer.DTO.PostExtenstions;
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
        private readonly IClerkUserService _userService;

        public PostController(IPostService postService, IClerkUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TweetResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostCreate(TweetRequest postDTO)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            postDTO.AuthorId = userId!;

            var postFromDb = await _postService.AddPost(postDTO);
            return CreatedAtAction(nameof(GetPostById), new { id = postFromDb.PostId }, postFromDb);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TweetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TweetResponse>> GetPostById(string id)
        {
            var post = await _postService.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TweetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TweetResponse>> PutPost(string id, TweetRequest postRequest)
        {
            var postFromDB = await _postService.Get(id);
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (postFromDB == null || postFromDB.AuthorId.ToString() != userId)
            {
                return NotFound();
            }
            else
            {
                var updatedPost = await _postService.UpdatePost(id, postRequest);
                return Ok(updatedPost);
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool),StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult> DeletePost(string id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            bool isDeleted = await _postService.DeletePost(id,userId!);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("getall")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<TweetResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<List<TweetResponse>>> GetAll()
        {
            var postsFrombDb = await _postService.GetAllPosts();
            if (postsFrombDb.Count() <= 0)
            {
                return NoContent();
            }
            else
            {
                var postsResponse = postsFrombDb.ToList();
                return Ok(postsResponse);
            }
        }
    }
}
