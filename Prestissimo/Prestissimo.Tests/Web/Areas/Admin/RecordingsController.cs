namespace Prestissimo.Tests.Web.Areas.Admin
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Prestissimo.Web;
    using System.Linq;
    using Xunit;

    public class RecordingsController
    {
        [Fact]
        public void RecordingsControllerShouldBeInAdminArea()
        {
            // Arrange
            var controller = typeof(Prestissimo.Web.Areas.Admin.Controllers.RecordingsController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.RouteValue.Should().Be(WebConstants.AdminArea);
        }

        [Fact]
        public void RecordingsControllerShouldBeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(Prestissimo.Web.Areas.Admin.Controllers.RecordingsController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.Roles.Should().Be(WebConstants.AdministratorRole);
        }
    }
}
