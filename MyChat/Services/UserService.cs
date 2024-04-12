using Microsoft.AspNetCore.Identity;
using MyChat.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyChat.Settings;

namespace MyChat.Services
{
    public interface IUserService
    {
        Task<IActionResult> RegisterUser(RegisterRequestDTO model);
        Task<IActionResult> Login(LoginRequestDTO model);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> RegisterUser(RegisterRequestDTO model)
        {
            var user = new IdentityUser { UserName = model.Name, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                var responseDTO = new LoginResponseDTO { Token = token };
                return new OkObjectResult(responseDTO);
            }

            return new BadRequestObjectResult(new { Message = "Registration failed", Errors = result.Errors });
        }

        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = GenerateJwtToken(user);
                var responseDTO = new LoginResponseDTO { Email = model.Email, Token = token };
                return new OkObjectResult(responseDTO);
            }

            return new UnauthorizedObjectResult(new { Message = "Invalid credentials" });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(jwtSettings.MinutesToExpiration);

            var token = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
