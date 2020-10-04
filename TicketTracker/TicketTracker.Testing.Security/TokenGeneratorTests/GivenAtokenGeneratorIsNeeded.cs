using System;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketTracker.Infrastructure.Security.Interfaces;
using TicketTracker.Infrastructure.Security.Services;
using TicketTracker.Testing.Security.Common;

namespace TicketTracker.Testing.Security.TokenGeneratorTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAtokenGeneratorIsNeeded : AbstractConfigurationBase
    {
        [TestMethod]
        public void WhenConstructed_WithValidArguments_ConstructsProperly()
        {
            // Arrange
            var configuration = GetConfiguration();

            // Act
            var result = new TokenGenerator(configuration);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ITokenGenerator));
        }

        [TestMethod]
        public void WhenConstructed_WithNullArguments_ThrowsNullException()
        {
            // Arrange
            IConfiguration configuration = null;

            // Act
            Action test = () =>
                new TokenGenerator(configuration);

            // Assert
            var exception = Assert.ThrowsException<ArgumentNullException>(test);
            Assert.AreEqual(nameof(configuration), exception.ParamName);
        }
    }
}
