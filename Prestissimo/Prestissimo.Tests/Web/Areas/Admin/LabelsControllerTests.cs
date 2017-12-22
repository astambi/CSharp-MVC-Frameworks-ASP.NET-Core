namespace Prestissimo.Tests.Web.Areas.Admin
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Prestissimo.Web;
    using Prestissimo.Web.Areas.Admin.Controllers;
    using System.Linq;
    using Xunit;

    public class LabelsControllerTests
    {
        [Fact]
        public void LabelsControllerShouldBeInAdminArea()
        {
            // Arrange
            var controller = typeof(LabelsController);

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
        public void LabelsControllerShouldBeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(LabelsController);

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
