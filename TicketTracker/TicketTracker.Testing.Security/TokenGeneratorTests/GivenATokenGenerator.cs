using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
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
    public class GivenATokenGenerator : AbstractConfigurationBase
    {
        private ITokenGenerator _tokenGenerator { get; set; }
        private ICurrentDateTime _currentDateTime { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _currentDateTime = new Mock<ICurrentDateTime>().Object;
            _tokenGenerator = new TokenGenerator(GetConfiguration(), _currentDateTime);
        }

        [TestMethod]
        public void WhenClaimsArgument_IsNull_ThrowsNullException()
        {
            // Arrange
            IEnumerable<Claim> claims = null;

            // Act
            Action test = () =>
                _tokenGenerator.CreateToken(claims);

            // Assert
            var exception = Assert.ThrowsException<ArgumentNullException>(test);
            Assert.AreEqual(nameof(claims), exception.ParamName);
        }

        [TestMethod]
        public void WhenClaimsArgument_IsEmptyList_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            IEnumerable<Claim> claims = new List<Claim>();

            // Act
            Action test = () =>
                _tokenGenerator.CreateToken(claims);

            // Assert
            var exception = Assert.ThrowsException<ArgumentOutOfRangeException>(test);
            Assert.AreEqual(nameof(claims), exception.ParamName);
        }

        [TestMethod]
        public void WhenClaimsGiven_AreValid_ReturnsValidJwtToken()
        {
            // Arrange
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Manager")
            };
            var config = GetConfiguration();

            // Act  
            var result = _tokenGenerator.CreateToken(claims);
            var token = new JwtSecurityToken(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(config["JWT:Issuer"], token.Issuer);
            Assert.AreEqual(config["JWT:Audience"], token.Audiences.First());
            Assert.AreEqual(SecurityAlgorithms.HmacSha256, token.SignatureAlgorithm);

            for (int i = 0; i < claims.Count; i++)
            {
                Assert.AreEqual
                (
                    claims[i].Type,
                    token.Claims.ElementAt(i).Type
                );
                Assert.AreEqual
                (
                    claims[i].Value,
                    token.Claims.ElementAt(i).Value
                );
            }
        }
    }
}
