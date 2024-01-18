using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using NuGet.Common;
using QuizAPI.DTOs.Account;
using QuizAPI.DTOs.Quzzes;
using QuizAPI.Entites;
using QuizAPI.Services.Abstract;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        //private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            //_signInManager = signInManager;

        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto dto, [FromServices] IJwtTokenService jwtTokenService)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, dto.Password)))
            {
                return Unauthorized("Invalid credentials");
            }

            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            var token = jwtTokenService.GenerateToken(user.FirstName, user.LastName, user.UserName, roles);
            return Ok(token);
        }

        [HttpPost("SignUp")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto dto)
        {
            var userEntity = _mapper.Map<AppUser>(dto);

            var result = await _userManager.CreateAsync(userEntity, dto.Password);

            if (result.Succeeded)
            {
                if (dto.IsAdmin)
                {
                    await _userManager.AddToRoleAsync(userEntity, "Admin");
                }
                else if (dto.IsUser)
                {
                    await _userManager.AddToRoleAsync(userEntity, "User");
                }

                return Ok("Account created successfully");
            }
            else
            {
                return BadRequest(result.Errors.Select(error => error.Description));
            }

        }
    }
}
