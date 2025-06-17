using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Threads.BusinessLogicLayer.DTO.CommentDTO;
using Threads.BusinessLogicLayer.DTO.PostDTO;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

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

        [HttpPost]
        [ProducesResponseType(typeof(List<CommentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentResponse>> CreateComment(CommentRequest commentRequest)
        {
            var postFromDb = await _postService.Get(commentRequest.PostId!);

            if (postFromDb == null)
            {
                return NotFound();
            }
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            commentRequest.AuthorId = userId;

            var addedComment = await _commentService.CreateComment(commentRequest);
            return CreatedAtAction(nameof(GetCommentById), new { id = addedComment?.CommentId }, addedComment);


        }

        [HttpGet("comment/{id}")]
        [ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentResponse>> GetCommentById(string id)
        {
            var comment = await _commentService.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpGet("replies/{id}")]
        [ProducesResponseType(typeof(List<CommentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<List<CommentResponse>>> GetCommentReplies(string parentId)
        {
            var parentFromDb = await _commentService.GetCommentById(parentId);

            if (parentFromDb == null)
            {
                return NotFound();
            }

            List<CommentResponse>? childs = await _commentService
                .GetCommentReplies(parentId);

            return Ok(childs);
        }

    }
}
