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
        public async Task<ActionResult<AuthModel>> PostRegister(Register register)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = register.UserName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                PersonName = register.PersonName,

            };

            var result = await _userManager.CreateAsync(applicationUser, register.Password);

            if(!result.Succeeded)
            {
                var errorMessage = String.Join(",", result.Errors.Select(e => e.Description));
                BadRequest(errorMessage);
            }
            await _userManager.AddToRoleAsync(applicationUser, Statics.User_Role);
            await _signInManager.SignInAsync(applicationUser, false);

            var jwtSecurityToken = await _authService.CreateJwtToken(applicationUser);


            var roles = await _userManager.GetRolesAsync(applicationUser);

            var authResponse = new AuthModel
            {
                Email = applicationUser.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthonticated = true,
                Roles = roles.ToList(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = applicationUser.UserName,
            };

            return Ok(authResponse);
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
