using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicketTracker.Application.Interfaces.Security;
using TicketTracker.Infrastructure.Security.Interfaces;
using TicketTracker.Infrastructure.Security.Models;
using TicketTracker.Infrastructure.Security.Services;
using TicketTracker.Testing.Security.Common;

namespace TicketTracker.Testing.Security.AuthenticatorTests
{
    [TestClass]
    public class GivenAnAuthenticatorIsNeeded : AuthenticationBaseTest
    {
        [TestMethod]
        public void WhenConstructed_WithValidArguments_ConstructsProperly()
        {
            // Arrange
            var userManager = CreateMockUserManager();
            var existingUser = new Mock<IExistingUser>(MockBehavior.Strict).Object;
            var tokenGenerator = new Mock<ITokenGenerator>(MockBehavior.Strict).Object;

            // Act
            var result = new Authenticator(userManager, existingUser, tokenGenerator);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IAuthenticator));
        }
    }
}
