namespace Prestissimo.Tests.Web.Controllers
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Prestissimo.Web.Controllers;
    using System.Linq;
    using Xunit;

    public class ShoppingCartControllerTests
    {
        [Fact]
        public void FinishOrderShouldBeOnlyForAuthorizedUsers()
        {
            // Arrange
            var method = typeof(ShoppingCartController)
                        .GetMethod(nameof(ShoppingCartController.FinishOrder));

            // Act
            var authorizeAttribute = method
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            authorizeAttribute.Should().NotBeNull();
        }

        [Fact]
        public void FinishOrderShouldHaveHttpPostAttribute()
        {
            // Arrange
            var method = typeof(ShoppingCartController)
                        .GetMethod(nameof(ShoppingCartController.FinishOrder));

            // Act
            var httpPostAttribute = method
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(HttpPostAttribute))
                as HttpPostAttribute;

            // Assert
            httpPostAttribute.Should().NotBeNull();
        }
    }
}
