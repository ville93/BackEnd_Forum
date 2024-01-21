using Microsoft.Extensions.Configuration;
using Moq;

namespace MyChat.Mocks
{
    public static class ConfigurationMock
    {
        public static Mock<IConfiguration> GetMockConfiguration()
        {
            var configuration = new Mock<IConfiguration>();

            configuration.SetupGet(x => x["JwtKey"]).Returns("your_secret_key");
            configuration.SetupGet(x => x["JwtIssuer"]).Returns("your_issuer");
            configuration.SetupGet(x => x["JwtExpireDays"]).Returns("1"); 

            return configuration;
        }
    }
}

