using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoccoEmFrente.Kanban.Api.Configurations;
using FoccoEmFrente.Kanban.Api.Controllers.Attributes;
using FoccoEmFrente.Kanban.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoccoEmFrente.Kanban.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModelState]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(ir => ir.Description);
                return BadRequest(string.Join("; ", errors));
            }

            return Ok(GetFullJwt());
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                var fullJwt = GetFullJwt();
                return Ok(fullJwt);
            }

            if (result.IsLockedOut)
            {
                return BadRequest("This user is temporarily blocked");
            }

            return BadRequest("Incorrect user or password");
        }

        private string GetFullJwt()
        {
            var tokenDescription = GetSecurityTokenDescriptor();
            return GenerateToken(tokenDescription);
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor()
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddMonths(12)
            };

            return tokenDescription;
        }

        private string GenerateToken(SecurityTokenDescriptor tokenDescription)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            return tokenHandle.WriteToken(tokenHandle.CreateToken(tokenDescription));
        }
    }
}
