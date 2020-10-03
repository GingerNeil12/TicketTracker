using System;
using System.Collections.Generic;
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
    [TestCategory(TestCategories.Unit)]
    public class GivenAnAuthenticatorIsNeeded : AuthenticationBaseTest
    {
        [TestMethod]
        public void WhenConstructed_WithValidArguments_ConstructsProperly()
        {
            // Arrange
            var userManager = CreateMockUserManager().Object;
            var existingUser = new Mock<IExistingUser>(MockBehavior.Default).Object;
            var tokenGenerator = new Mock<ITokenGenerator>(MockBehavior.Default).Object;

            // Act
            var result = new Authenticator(userManager, existingUser, tokenGenerator);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IAuthenticator));
        }

        [TestMethod]
        [DynamicData(nameof(GetNullArguments), DynamicDataSourceType.Method)]
        public void WhenConstructed_WithNullArguments_ThrowsNullException
        (
            string expectedParamName,
            UserManager<ApplicationUser> userManager,
            IExistingUser existingUser,
            ITokenGenerator tokenGenerator
        )
        {
            // Arrange

            // Act
            Action test = () =>
                new Authenticator(userManager, existingUser, tokenGenerator);

            // Assert
            var exception = Assert.ThrowsException<ArgumentNullException>(test);
            Assert.AreEqual(expectedParamName, exception.ParamName);
        }

        private static IEnumerable<object[]> GetNullArguments()
        {
            var userManager = CreateMockUserManager().Object;
            var existingUser = new Mock<IExistingUser>(MockBehavior.Default).Object;
            var tokenGenerator = new Mock<ITokenGenerator>(MockBehavior.Default).Object;

            yield return new object[] { nameof(userManager), null, existingUser, tokenGenerator };
            yield return new object[] { nameof(existingUser), userManager, null, tokenGenerator };
            yield return new object[] { nameof(tokenGenerator), userManager, existingUser, null };
        }
    }
}
