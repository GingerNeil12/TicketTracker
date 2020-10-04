using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicketTracker.Application.Interfaces.Common;
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
            var currentDateTime = new Mock<ICurrentDateTime>().Object;

            // Act
            var result = new TokenGenerator(configuration, currentDateTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ITokenGenerator));
        }

        [TestMethod]
        [DynamicData(nameof(GetNullArguments), DynamicDataSourceType.Method)]
        public void WhenConstructed_WithNullArguments_ThrowsNullException
        (
            string expectedParamName,
            IConfiguration configuration,
            ICurrentDateTime currentDateTime
        )
        {
            // Arrange

            // Act
            Action test = () =>
                new TokenGenerator(configuration, currentDateTime);

            // Assert
            var exception = Assert.ThrowsException<ArgumentNullException>(test);
            Assert.AreEqual(expectedParamName, exception.ParamName);
        }

        private static IEnumerable<object[]> GetNullArguments()
        {
            var configuration = GetConfiguration();
            var currentDateTime = new Mock<ICurrentDateTime>().Object;

            yield return new object[] { nameof(configuration), null, currentDateTime };
            yield return new object[] { nameof(currentDateTime), configuration, null };
        }
    }
}
