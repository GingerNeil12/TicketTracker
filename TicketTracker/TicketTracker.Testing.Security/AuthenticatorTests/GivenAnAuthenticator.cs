using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicketTracker.Application.Interfaces.Security;
using TicketTracker.Application.Models.Security;
using TicketTracker.Infrastructure.Security.Interfaces;
using TicketTracker.Infrastructure.Security.Models;
using TicketTracker.Infrastructure.Security.Services;
using TicketTracker.Shared.ViewModels.Security;
using TicketTracker.Testing.Security.Common;

namespace TicketTracker.Testing.Security.AuthenticatorTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAnAuthenticator : AbstractAuthenticationBase
    {
        private Mock<UserManager<ApplicationUser>> _userManagerMock { get; set; }
        private Mock<IExistingUser> _existingUserMock { get; set; }
        private Mock<ITokenGenerator> _tokenGeneratorMock { get; set; }
        private IAuthenticator _authenticator { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _existingUserMock = new Mock<IExistingUser>();
            _tokenGeneratorMock = new Mock<ITokenGenerator>();

            _userManagerMock = CreateMockUserManager();
        }

        [TestMethod]
        public async Task WhenInputEmail_IsInvalid_ReturnsEmailOrPasswordIncorrectResponseAsync()
        {
            // Arrange            
            ApplicationUser user = null;
            _userManagerMock.Setup
            (
                x => x.FindByEmailAsync(It.IsAny<string>())
            )
            .ReturnsAsync(user);

            _authenticator = CreateAuthenticator();

            await AssertEmailOrPasswordIncorrectErrorResponse();
        }

        [TestMethod]
        public async Task WhenAccountIsLockedOut_ReturnsAccountLockedResponseAsync()
        {
            // Arrange
            _userManagerMock.Setup
            (
                x => x.FindByEmailAsync(It.Is<string>(y => y.Equals(Defaults.EmailAddress)))
            ).ReturnsAsync(new ApplicationUser());
            _userManagerMock.Setup
            (
                x => x.IsLockedOutAsync(It.IsAny<ApplicationUser>())
            ).ReturnsAsync(true);

            _authenticator = CreateAuthenticator();

            // Act
            var result = await _authenticator.AuthenticateUserAsync(CreateLoginDto());

            // Assert
            Assert.AreEqual(AuthenticationCode.AccountLocked, result.Code);
            Assert.AreEqual(Defaults.ErrorMessages.AccountLocked, result.Message);
        }

        [TestMethod]
        public async Task WhenInputPassword_IsInvalid_ReturnsEmailOrPasswordIncorrectResponseAsync()
        {
            // Arrange
            _userManagerMock.Setup
            (
                x => x.FindByEmailAsync(It.Is<string>(y => y.Equals(Defaults.EmailAddress)))
            ).ReturnsAsync(new ApplicationUser());
            _userManagerMock.Setup
            (
                x => x.IsLockedOutAsync(It.IsAny<ApplicationUser>())
            ).ReturnsAsync(false);
            _userManagerMock.Setup
            (
                x => x.CheckPasswordAsync
                (
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<string>()
                )
            ).ReturnsAsync(false);

            _authenticator = CreateAuthenticator();

            await AssertEmailOrPasswordIncorrectErrorResponse();
        }

        [TestMethod]
        public async Task WhenInputIsValid_AndAccountNotLockedOut_ShouldReturnSuccessStatusCodeAsync()
        {
            // Arrange UserManager
            _userManagerMock.Setup
            (
                x => x.FindByEmailAsync(It.Is<string>(y => y.Equals(Defaults.EmailAddress)))
            ).ReturnsAsync(new ApplicationUser());

            _userManagerMock.Setup
            (
                x => x.IsLockedOutAsync(It.IsAny<ApplicationUser>())
            ).ReturnsAsync(false);
            
            _userManagerMock.Setup
            (
                x => x.CheckPasswordAsync
                (
                    It.IsAny<ApplicationUser>(),
                    It.Is<string>(y => y.Equals(Defaults.Password))
                )
            ).ReturnsAsync(true);

            // Arrange Existing User
            _existingUserMock.Setup
            (
                x => x.GetAccessClaimsForUserAsync(It.IsAny<string>())
            ).ReturnsAsync(new List<Claim>());

            // Arrange TokenGenerator
            var token = "Token";
            _tokenGeneratorMock.Setup
            (
                x => x.CreateToken(It.IsAny<IEnumerable<Claim>>())
            ).Returns(token);

            // Create Authenticator
            _authenticator = CreateAuthenticator();

            // Act
            var result = await _authenticator.AuthenticateUserAsync(CreateLoginDto());

            // Assert
            Assert.AreEqual(AuthenticationCode.Success, result.Code);
            Assert.AreEqual(token, result.Message);
        }

        private IAuthenticator CreateAuthenticator()
        {
            return new Authenticator
            (
                _userManagerMock.Object,
                _existingUserMock.Object,
                _tokenGeneratorMock.Object
            );
        }

        private async Task AssertEmailOrPasswordIncorrectErrorResponse()
        {
            // Act
            var result = await _authenticator.AuthenticateUserAsync(CreateLoginDto());

            // Assert
            Assert.AreEqual(AuthenticationCode.EmailOrPasswordIncorrect, result.Code);
            Assert.AreEqual(Defaults.ErrorMessages.EmailOrPasswordIncorrect, result.Message);
        }

        private static LoginDto CreateLoginDto()
        {
            return new LoginDto
            {
                EmailAddress = Defaults.EmailAddress,
                Password = Defaults.Password
            };
        }

        private static class Defaults
        {
            public const string EmailAddress = "example@example.com";
            public const string Password = "Password";

            public static class ErrorMessages
            {
                public const string EmailOrPasswordIncorrect = "Email or Password is incorrect.";
                public const string AccountLocked = "Account locked.";
            }
        }
    }
}
