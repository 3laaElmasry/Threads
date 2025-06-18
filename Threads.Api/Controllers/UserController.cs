using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.BusinessLogicLayer.Services;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IClerkUserService _clerkUserService;

        public UserController(IClerkUserService clerkUserService)
        {
            _clerkUserService = clerkUserService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfile))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserProfile>> GetMyProfile()
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (clerkUserId is null)
                return Unauthorized();

            var user = await _clerkUserService.GetUserAsync(clerkUserId);

            if (user == null)
            {
                return NotFound();
            }

            return user.ToUserProfile();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserProfile>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("{userName}")]
        public async Task<ActionResult<List<UserProfile>>> SearchForUsersByUserName(string userName)
        {
            var res = await _clerkUserService.GetUsersByUsernameAsync(userName);

            if (res.Count() <= 0)
            {
                return NotFound();
            }
            return res;
        }



    }
}
