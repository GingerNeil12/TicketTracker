using System;
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
    public class GivenAnAuthenticator : AuthenticationBaseTest
    {
        private Mock<UserManager<ApplicationUser>> _userManagerMock { get; set; }
        private Mock<IExistingUser> _existingUserMock { get; set; }
        private Mock<ITokenGenerator> _tokenGeneratorMock { get; set; }
        private IAuthenticator _authenticator { get; set; }

        [TestMethod]
        public async Task WhenInputEmail_IsInvalid_ReturnsEmailOrPasswordIncorrectResponseAsync()
        {
            // Arrange
            _existingUserMock = new Mock<IExistingUser>();
            _tokenGeneratorMock = new Mock<ITokenGenerator>();
            
            ApplicationUser user = null;
            _userManagerMock = CreateMockUserManager();
            _userManagerMock.Setup
            (
                x => x.FindByEmailAsync(It.IsAny<string>())
            )
            .ReturnsAsync(user);

            _authenticator = new Authenticator
            (
                _userManagerMock.Object,
                _existingUserMock.Object,
                _tokenGeneratorMock.Object
            );

            var loginDto = new LoginDto
            {
                EmailAddress = "example@example.com",
                Password = "Password"
            };

            // Act
            var result = await _authenticator.AuthenticateUserAsync(loginDto);

            // Assert
            Assert.AreEqual(AuthenticationCode.EmailOrPasswordIncorrect, result.Code);
            Assert.AreEqual("Email or Password is incorrect.", result.Message);
        }

        [TestMethod]
        public async Task WhenAccountIsLockedOut_ReturnsAccountLockedResponseAsync()
        {
            // Arrange
            _existingUserMock = new Mock<IExistingUser>();
            _tokenGeneratorMock = new Mock<ITokenGenerator>();

            _userManagerMock = new Mock<UserManager<ApplicationUser>>();
            _userManagerMock.Setup
            (
                x => x.FindByEmailAsync(It.Is<string>(y => y.Equals("")))
            );
        }
    }
}
