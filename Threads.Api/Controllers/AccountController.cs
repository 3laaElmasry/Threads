using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.BusinessLogicLayer.Models;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAuthService _authService;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authService = authService;
        }


        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthModel>> PostRegister([FromBody]Register user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.RegisterAsync(user);

            if (!result.IsAuthonticated)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthModel>> PostLogIn([FromBody] UserLoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.GetJwtTokenAsync(user);

            if (!result.IsAuthonticated)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }


        [HttpGet("getall")]
        [Authorize]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<UserResponse>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users.Count <= 0)
            {
                return NoContent();
            }
            var registerResponses = users.Select(u => u.ToRegisterResponse());
            return Ok(registerResponses);
        }

        [HttpGet("users/{userName}")]
        [Authorize]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<UserResponse>>> GetUsersByUserName(string userName)
        {
            var usersWithUserName = await _userManager.Users
                .Where(u => u.UserName!.Contains(userName)).ToListAsync();

            if (usersWithUserName.Count <= 0)
            {
                return NoContent();
            }

            List<UserResponse> responseUsers = usersWithUserName.Select(u => u.ToRegisterResponse()).ToList();

            return Ok(responseUsers);
        }

        [HttpGet("check-email/{email}")]
        public async Task<ActionResult> IsEmailAlreadyExist(string email)

        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpGet("check-username/{userName}")]
        public async Task<ActionResult> IsUserNameAlreadyExist(string userName)

        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
