using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Threads.BusinessLogicLayer.Services;

namespace Threads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IClerkUserService _userService;

        public UserController(IClerkUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (clerkUserId is null)
                return Unauthorized();

            var user = await _userService.GetUserAsync(clerkUserId);

            return Ok(new
            {
                user?.Id,
                user?.Username,
                Email = user?.EmailAddresses?.FirstOrDefault()?.EmailAddressValue,
                user?.FirstName,
                user?.LastName,
                user?.ImageUrl
            });
        }


    }
}
