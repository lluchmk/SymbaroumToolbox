using Abilities.Application.Users;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Abilities.Application.Tests.Users
{
    public class UsersServiceTests
    {
        [Fact]
        public void IsUserAuthenticated_WhenUserAuthenticated_ReturnsTrue()
        {
            var context = new DefaultHttpContext();
            var identity = new ClaimsIdentity("bearer");
            context.User = new ClaimsPrincipal(identity);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.SetupGet(c => c.HttpContext)
                .Returns(context);

            var sut = new UsersService(contextAccessor.Object);

            var response = sut.IsUserAuthenticated();

            response.Should().BeTrue();
        }

        [Fact]
        public void IsUserAuthenticated_WhenUserNotAuthenticated_ReturnsFals()
        {
            var context = new DefaultHttpContext();
            var identity = new ClaimsIdentity();
            context.User = new ClaimsPrincipal(identity);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.SetupGet(c => c.HttpContext)
                .Returns(context);

            var sut = new UsersService(contextAccessor.Object);

            var response = sut.IsUserAuthenticated();

            response.Should().BeFalse();
        }

        [Fact]
        public void GetUserId_ReturnsCurrentUserId()
        {
            var context = new DefaultHttpContext();

            var userId = "TheUserId";
            var userClaims = new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, userId) };
            var identity = new ClaimsIdentity(userClaims);
            context.User = new ClaimsPrincipal(identity);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.SetupGet(c => c.HttpContext)
                .Returns(context);

            var sut = new UsersService(contextAccessor.Object);

            var response = sut.GetUserId();

            response.Should().Be(userId);
        }
    }
}
