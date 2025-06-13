using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
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

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegisterResponse>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToRegisterResponse());
        }


        [HttpPost]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegisterResponse>> PostRegister(Register register)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = register.UserName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                
            };

            var result = await _userManager.CreateAsync(applicationUser,register.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(applicationUser,false);
                return CreatedAtAction(nameof(GetUserById), new { id = applicationUser.Id }, applicationUser.ToRegisterResponse());

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }


        [HttpGet("getall")]
        [Authorize]
        [ProducesResponseType(typeof(List<RegisterResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<RegisterResponse>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users.IsNullOrEmpty())
            {
                return NoContent();
            }
            var registerResponses = users.Select(u => u.ToRegisterResponse());
            return Ok(registerResponses);
        }

        


    }
}
