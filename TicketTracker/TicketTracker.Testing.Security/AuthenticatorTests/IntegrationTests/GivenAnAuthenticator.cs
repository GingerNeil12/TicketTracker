using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketTracker.Application.Interfaces.Security;
using TicketTracker.Application.Models.Security;
using TicketTracker.Infrastructure.Security.Services;
using TicketTracker.Infrastructure.Services;
using TicketTracker.Shared.ViewModels.Security;
using TicketTracker.Testing.Security.Common;

namespace TicketTracker.Testing.Security.AuthenticatorTests.IntegrationTests
{
    [TestClass]
    [TestCategory(TestCategories.Integration)]
    public class GivenAnAuthenticator : AbstractAuthenticationBase
    {
        private const int TotalNameIdentifierClaimCount = 1;
        private const int TotalRefreshTokenClaimCount = 1;
        private const int TotalAdminRoleCount = 1;

        private IAuthenticator _authenticator { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            CreateAdminAccount().Wait();
            CreateStandardUserAccount().Wait();
            
            var currentDateTime = new CurrentDateTime();
            
            var existingUser = new ExistingUser
            (
                GetNonMockUserManager(),
                currentDateTime
            );
            
            var tokenGenerator = new TokenGenerator
            (
                GetConfiguration(),
                currentDateTime
            );

            _authenticator = new Authenticator
            (
                GetNonMockUserManager(),
                existingUser,
                tokenGenerator
            );
        }

        [TestMethod]
        public async Task WhenAdminCredentialsAreValid_ReturnsSuccessResponseAndAccessTokenAsync()
        {
            // Arrange
            var loginDto = new LoginDto()
            {
                EmailAddress = AdminUserEmail,
                Password = AdminPassword
            };
            var config = GetConfiguration();

            // Act
            var result = await _authenticator.AuthenticateUserAsync(loginDto);
            var token = GetTokenObjectFromTokenString(result.Message);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(config["JWT:Issuer"], token.Issuer);
            Assert.AreEqual(config["JWT:Audience"], token.Audiences.First());
            Assert.AreEqual(SecurityAlgorithms.HmacSha256, token.SignatureAlgorithm);
            Assert.AreEqual(AuthenticationCode.Success, result.Code);
            Assert.IsNotNull(token.Claims);

            Assert.AreEqual
            (
                TotalNameIdentifierClaimCount,
                token.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).ToList().Count
            );

            Assert.AreEqual
            (
                TotalRefreshTokenClaimCount,
                token.Claims.Where(x => x.Type == "RefreshToken").ToList().Count
            );

            Assert.AreEqual
            (
                TotalAdminRoleCount,
                token.Claims.Where(x => x.Type == ClaimTypes.Role).ToList().Count
            );

            Assert.AreEqual
            (
                AdminRoleName,
                token.Claims.Where(x => x.Type == ClaimTypes.Role).First().Value
            );
        }

        [TestMethod]
        public async Task WhenStandardCredentialsAreValid_ReturnsSuccessResponseAndAccessTokenAsync()
        {
            // Arrange
            var loginDto = new LoginDto()
            {
                EmailAddress = StandardUserEmail,
                Password = StandardPassword
            };
            var config = GetConfiguration();

            // Act
            var result = await _authenticator.AuthenticateUserAsync(loginDto);
            var token = GetTokenObjectFromTokenString(result.Message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Message);
            Assert.AreEqual(AuthenticationCode.Success, result.Code);
            Assert.AreEqual(config["JWT:Issuer"], token.Issuer);
            Assert.AreEqual(config["JWT:Audience"], token.Audiences.First());
            Assert.AreEqual(SecurityAlgorithms.HmacSha256, token.SignatureAlgorithm);
            Assert.IsNotNull(token.Claims);

            Assert.AreEqual
            (
                TotalNameIdentifierClaimCount,
                token.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).ToList().Count
            );

            Assert.AreEqual
            (
                TotalRefreshTokenClaimCount,
                token.Claims.Where(x => x.Type == "RefreshToken").ToList().Count
            );

            Assert.AreEqual
            (
                TotalAdminRoleCount,
                token.Claims.Where(x => x.Type == ClaimTypes.Role).ToList().Count
            );

            Assert.AreEqual
            (
                StandardRoleName,
                token.Claims.Where(x => x.Type == ClaimTypes.Role).First().Value
            );
        }

        [TestMethod]
        public async Task WhenAdminEmailIsValid_AndPasswordIsNot_ReturnsEmailOrPasswordIncorrectResponseAsync()
        {
            // Arrange
            var loginDto = new LoginDto()
            {
                EmailAddress = AdminUserEmail,
                Password = StandardPassword
            };

            // Act
            var result = await _authenticator.AuthenticateUserAsync(loginDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(AuthenticationCode.EmailOrPasswordIncorrect, result.Code);
            Assert.AreEqual("Email or Password is incorrect.", result.Message);
        }
    }
}
