using AutoMapper;
using BottomTimeAuth.DTOs;
using BottomTimeAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BottomTimeAuth.Controllers {
	[Route("auth")]
	[ApiController]
	public class AuthController : ControllerBase {
		private readonly ILogger<AuthController> _logger;
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AuthController(ILogger<AuthController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) {
			_logger = logger;
			_mapper = mapper;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) {
			if (await UserExists(registerDto.UserName)) {
				return BadRequest("Username is taken.");
			}

			AppUser user = _mapper.Map<AppUser>(registerDto);

			user.UserName = registerDto.UserName.ToLower();

			IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

			if (!result.Succeeded) {
				_logger.LogError("An error occurred during user registration.", result);

				return BadRequest(result.Errors);
			}

			IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "Member");

			return roleResult.Succeeded ? new UserDto { UserName = user.UserName } : BadRequest(result.Errors);
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) {
			AppUser user = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.UserName.ToLower());

			if (user == null) {
				return Unauthorized("Invalid username.");
			}

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			return result.Succeeded ? new UserDto { UserName = user.UserName } : Unauthorized();
		}

		private async Task<bool> UserExists(string username) {
			return await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower());
		}
	}
}
