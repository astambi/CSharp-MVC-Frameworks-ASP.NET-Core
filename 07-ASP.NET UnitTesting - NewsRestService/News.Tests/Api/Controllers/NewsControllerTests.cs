namespace News.Tests.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using News.Api.Controllers;
    using News.Api.Models.News;
    using News.Data;
    using News.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Xunit;

    public class NewsControllerTests
    {
        private NewsDbContext Context
        {
            get
            {
                var dbOptions = new DbContextOptionsBuilder<NewsDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new NewsDbContext(dbOptions);
            }
        }

        [Fact]
        public void NewsController_ShouldHaveRouteAttribute()
        {
            // Arrange
            var controllerType = typeof(NewsController);

            // Act
            var attributes = controllerType.GetCustomAttributes(true);

            // Assert
            Assert.True(attributes.Any(a => a.GetType() == typeof(RouteAttribute)));
        }

        [Fact]
        public void GetAllNews_ShouldHaveHttpGetAttribute()
        {
            // Arrange
            var method = typeof(NewsController)
                        .GetMethod(nameof(NewsController.GetAllNews));

            // Act
            var attributes = method.GetCustomAttributes(true);

            // Assert
            Assert.True(attributes.Any(a => a.GetType() == typeof(HttpGetAttribute)));
        }

        [Fact]
        public void GetAllNews_ShouldReturnOkStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            // Act
            var result = newsController.GetAllNews();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllNews_ShouldReturnCorrectData()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            // Act
            var returnedModels = (newsController.GetAllNews() as OkObjectResult)
                                .Value as IEnumerable<News>;

            // Assert
            var testData = this.GetTestData();
            foreach (var returnedModel in returnedModels)
            {
                var testModel = testData.FirstOrDefault(d => d.Id == returnedModel.Id);

                Assert.NotNull(testModel);
                Assert.True(this.CompareNewsExact(returnedModel, testModel));
            }
        }

        [Fact]
        public void GetSingleNews_ShouldHaveHttpGetAttribute()
        {
            // Arrange
            var method = typeof(NewsController)
                        .GetMethod(nameof(NewsController.GetSingleNews));

            // Act
            var attributes = method.GetCustomAttributes(true);

            // Assert
            Assert.True(attributes.Any(a => a.GetType() == typeof(HttpGetAttribute)));
        }

        [Fact]
        public void GetSingleNewsWithIncorrectData_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var incorrectId = 100;

            // Act
            var result = newsController.GetSingleNews(incorrectId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetSingleNewsWithCorrectData_ShouldReturnOkStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var correctId = this.GetTestData().First().Id;

            // Act
            var result = newsController.GetSingleNews(correctId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetSingleNewsWithCorrectData_ShouldReturnCorrectData()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var testModel = this.GetTestData().First();

            // Act
            var returnedModel = (newsController.GetSingleNews(testModel.Id) as OkObjectResult)
                                .Value as News;

            // Assert
            Assert.NotNull(returnedModel);
            Assert.True(this.CompareNewsExact(returnedModel, testModel));
        }

        [Fact]
        public void PostNews_ShouldHaveHttpPostAttribute()
        {
            // Arrange
            var method = typeof(NewsController)
                        .GetMethod(nameof(NewsController.PostNews));

            // Act
            var attributes = method.GetCustomAttributes(true);

            // Assert
            Assert.True(attributes.Any(a => a.GetType() == typeof(HttpPostAttribute)));
        }

        [Fact]
        public void PostNewsWithIncorrectData_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsController();

            var testData = this.GetTestData().First();
            testData.Title = null; // with Required Attribute

            newsController.ModelState.AddModelError("Invalid Data", "Invalid Data"); // NB testing for incorrect model state!

            // Act
            var result = newsController.PostNews(this.ProjectToNewsModel(testData));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PostNewsWithCorrectData_ShouldReturnCreatedStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsController();

            var testModel = this.GetTestData().First();

            // Act
            var result = newsController.PostNews(this.ProjectToNewsModel(testModel));

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void PostNewsWithCorrectData_ShouldReturnCorrectData()
        {
            // Arrange
            var newsController = this.GetNewsController();

            var testModel = this.GetTestData().First();

            // Act
            var returnedModel =
                (newsController.PostNews(this.ProjectToNewsModel(testModel)) as CreatedAtActionResult)
                .Value as News;

            // Assert
            Assert.NotNull(returnedModel);
            Assert.True(this.CompareNewsExact(returnedModel, testModel));
        }

        [Fact]
        public void PutNews_ShouldHaveHttpPutAttribute()
        {
            // Arrange
            var method = typeof(NewsController)
                        .GetMethod(nameof(NewsController.PutNews));

            // Act
            var attributes = method.GetCustomAttributes(true);

            // Assert
            Assert.True(attributes.Any(a => a.GetType() == typeof(HttpPutAttribute)));
        }

        [Fact]
        public void PutNewsWithIncorrectData_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var incorrectId = 100;
            var testModel = this.ProjectToNewsModel(this.GetTestData().First());

            // Act
            var result = newsController.PutNews(incorrectId, testModel);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutNewsWithInvalidModelState_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var testNews = this.GetTestData().First();
            testNews.Title = null;

            newsController.ModelState.AddModelError("Invalid Data", "Invalid Data"); // NB testing for incorrect model state!

            // Act
            var result = newsController.PutNews(testNews.Id, this.ProjectToNewsModel(testNews));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PutNewsWithCorrectData_ShouldReturnOkStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var testModel = this.GetTestData().First();
            testModel.Title = "Updated Title";
            testModel.Content = "Updated Content";
            testModel.PublishDate = DateTime.UtcNow;

            // Act
            var result = newsController.PutNews(testModel.Id, this.ProjectToNewsModel(testModel));

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void PutNewsWithCorrectData_ShouldUpdateItem()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var testModel = this.GetTestData().First();
            testModel.Title = "Updated Title";
            testModel.Content = "Updated Content";
            testModel.PublishDate = DateTime.UtcNow;

            // Act
            var result = newsController.PutNews(testModel.Id, this.ProjectToNewsModel(testModel));
            var returnedModel = (newsController.GetSingleNews(testModel.Id) as OkObjectResult)
                                .Value as News;

            // Assert
            Assert.NotNull(returnedModel);
            Assert.Equal(testModel.Id, returnedModel.Id);
            Assert.Equal(testModel.Title, returnedModel.Title);
            Assert.Equal(testModel.Content, returnedModel.Content);
            Assert.Equal(testModel.PublishDate, returnedModel.PublishDate);
        }
        [Fact]
        public void DeleteNews_ShouldHaveHttpDeleteAttribute()
        {
            // Arrange
            var method = typeof(NewsController)
                        .GetMethod(nameof(NewsController.DeleteNews));

            // Act
            var attributes = method.GetCustomAttributes(true);

            // Assert
            Assert.True(attributes.Any(a => a.GetType() == typeof(HttpDeleteAttribute)));
        }

        [Fact]
        public void DeleteNewsWithIncorrectData_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var incorrectId = 100;

            // Act
            var result = newsController.DeleteNews(incorrectId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteNewsWithCorrectData_ShouldReturnOkStatusCode()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var testModel = this.GetTestData().First();

            // Act
            var result = newsController.DeleteNews(testModel.Id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteNewsWithCorrectData_ShouldRemoveItem()
        {
            // Arrange
            var newsController = this.GetNewsControllerWithTestData();

            var testModel = this.GetTestData().First();

            // Act
            var result = newsController.DeleteNews(testModel.Id);
            var deletedModel = newsController.GetSingleNews(testModel.Id);

            // Assert
            Assert.IsType<NotFoundResult>(deletedModel);
        }

        private NewsModel ProjectToNewsModel(News model)
            => new NewsModel
            {
                Title = model.Title,
                Content = model.Content,
                PublishDate = model.PublishDate
            };

        private bool CompareNewsExact(News thisNews, News otherNews)
            => thisNews.Id == otherNews.Id
                && thisNews.Title == otherNews.Title
                && thisNews.Content == otherNews.Content
                && thisNews.PublishDate == otherNews.PublishDate;

        private NewsController GetNewsController()
            => new NewsController(this.Context);

        private NewsController GetNewsControllerWithTestData()
        {
            var context = this.Context;
            this.PopulateData(context);
            return new NewsController(context);
        }

        private void PopulateData(NewsDbContext context)
        {
            context.News.AddRange(this.GetTestData());
            context.SaveChanges();
        }

        private IEnumerable<News> GetTestData()
            => new List<News>
            {
                new News{
                    Id = 1,
                    Title = "Title1",
                    Content = "Content1",
                    PublishDate = DateTime.ParseExact("2017/12/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) },
                new News{
                    Id = 2,
                    Title = "Title2",
                    Content = "Content2",
                    PublishDate = DateTime.ParseExact("2017/09/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) },
                new News{
                    Id = 3,
                    Title = "Title3",
                    Content = "Content3",
                    PublishDate = DateTime.ParseExact("2016/09/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) },
                new News{
                    Id = 4,
                    Title = "Title4",
                    Content = "Content4",
                    PublishDate = DateTime.ParseExact("2016/06/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) },
                new News{
                    Id = 5,
                    Title = "Title5",
                    Content = "Content5",
                    PublishDate = DateTime.ParseExact("2016/01/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) }
            };
    }
}
