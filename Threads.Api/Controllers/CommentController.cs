using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Threads.BusinessLogicLayer.DTO.CommentDTO;
using Threads.BusinessLogicLayer.DTO.PostDTO;
using Threads.BusinessLogicLayer.ServiceContracts;

namespace Threads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        [HttpGet("{postId}")]
        [ProducesResponseType(typeof(List<CommentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CommentResponse>>> GetAll(string postId)
        {
            if (postId == null)
                return BadRequest("Post Id Is Required");

            var postFromDb = _postService.Get(postId);
            if(postFromDb == null)
            {
                return NotFound();
            }

            var comments = await _commentService.GetAll(postId);
            return Ok(comments);
        }
    }
}
