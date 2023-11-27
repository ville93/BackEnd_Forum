using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyChat.Tests.Mocks
{
    public static class UserManagerMock
    {
        public static Mock<UserManager<TUser>> GetMockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var users = new List<TUser>();

            var mockUserManager = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                           .ReturnsAsync((string email) => users.FirstOrDefault(u => u.GetType().GetProperty("Email").GetValue(u).ToString() == email));

            mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                           .ReturnsAsync(true); // Simuloi aina oikean salasanan tarkistus

            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                           .ReturnsAsync(IdentityResult.Success); // Simuloi aina onnistunut käyttäjän luominen

            mockUserManager.Setup(x => x.Users)
                           .Returns(users.AsQueryable);

            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<TUser>()))
                           .ReturnsAsync(IdentityResult.Success); // Simuloi aina onnistunut käyttäjän luominen

            return mockUserManager;
        }
    }
}
