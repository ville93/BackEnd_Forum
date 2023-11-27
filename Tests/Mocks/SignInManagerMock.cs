using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Moq;
using MyChat.Mocks;

namespace MyChat.Tests.Mocks
{
    public static class SignInManagerMock
    {
        public static Mock<SignInManager<TUser>> GetMockSignInManager<TUser>() where TUser : class
        {
            var userManager = UserManagerMock.GetMockUserManager<TUser>();
            var context = new Mock<Microsoft.AspNetCore.Http.HttpContext>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<TUser>>();
            var options = new Mock<Microsoft.Extensions.Options.IOptions<IdentityOptions>>();
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<SignInManager<TUser>>>();

            var signInManager = new Mock<SignInManager<TUser>>(
                userManager.Object,
                context.Object,
                userPrincipalFactory.Object,
                options.Object,
                logger.Object,
                new Mock<IAuthenticationSchemeProvider>().Object
            );

            return signInManager;
        }
    }
}
