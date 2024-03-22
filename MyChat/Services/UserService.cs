using Microsoft.AspNetCore.Identity;
using MyChat.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MyChat.Services
{
    public interface IUserService
    {
        Task<IActionResult> RegisterUser(RegisterRequestDTO model);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> RegisterUser(RegisterRequestDTO model)
        {
            var user = new IdentityUser { UserName = model.Name, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return new OkObjectResult(new { Message = "Registration successful" });
            }

            return new BadRequestObjectResult(new { Message = "Registration failed", Errors = result.Errors });
        }
    }
}
