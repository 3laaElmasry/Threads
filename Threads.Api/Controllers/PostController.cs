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
            postDTO.AuthorId = userId!;

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

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PostResponse>> PutPost(string id, PostRequest postRequest)
        {
            var postFromDB = await _postService.Get(id);

            if(postFromDB == null || postFromDB.AuthorId.ToString() != postRequest.AuthorId)
            {
                return NotFound();
            }
            else
            {
                var updatedPost = await _postService.UpdatePost(id, postRequest);
                return Ok(updatedPost);
            }

        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(bool),StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult> DeletePost(string postId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            bool isDeleted = await _postService.DeletePost(postId,userId!);
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
        [ProducesResponseType(typeof(List<PostResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<List<PostResponse>>> GetAll()
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
