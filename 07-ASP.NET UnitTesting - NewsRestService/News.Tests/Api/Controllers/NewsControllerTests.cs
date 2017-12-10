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
        public void GetAllNews_ShouldReturnOkStatusCode()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            var result = newsController.GetAllNews();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllNews_ShouldReturnCorrectData()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);
            var testData = GetTestData();

            // Act
            var returnedModels = (newsController.GetAllNews() as OkObjectResult)
                                .Value as IEnumerable<News>;

            // Assert
            foreach (var returnedModel in returnedModels)
            {
                var testModel = testData.FirstOrDefault(d => d.Id == returnedModel.Id);
                Assert.NotNull(testModel);
                Assert.True(this.CompareNewsExact(returnedModel, testModel));
            }
        }

        [Fact]
        public void GetSingleNewsWithIncorrectData_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            var incorrectId = 100;
            var result = newsController.GetSingleNews(incorrectId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetSingleNewsWithCorrectData_ShouldReturnOkStatusCode()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            var id = this.GetTestData().First().Id;
            var result = newsController.GetSingleNews(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetSingleNewsWithCorrectData_ShouldReturnCorrectData()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            var testModel = this.GetTestData().First();
            var returnedModel = (newsController.GetSingleNews(testModel.Id) as OkObjectResult)
                                .Value as News;

            // Assert
            Assert.NotNull(returnedModel);
            Assert.True(this.CompareNewsExact(testModel, returnedModel));
        }

        [Fact]
        public void PostNewsWithCorrectData_ShouldReturnCreatedStatusCode()
        {
            // Arrange
            var context = this.Context;
            var newsController = new NewsController(context);

            var testModel = this.GetTestData().First();
            Assert.NotNull(testModel);

            // Act
            var result = newsController.PostNews(this.ProjectToNewsModel(testModel));

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void PostNewsWithCorrectData_ShouldReturnCorrectData()
        {
            // Arrange
            var context = this.Context;
            var newsController = new NewsController(context);

            var testModel = this.GetTestData().First();
            Assert.NotNull(testModel);

            // Act
            var returnedModel =
                (newsController.PostNews(this.ProjectToNewsModel(testModel)) as CreatedAtActionResult)
                .Value as News;

            // Assert
            Assert.NotNull(returnedModel);
            Assert.True(this.CompareNewsExact(returnedModel, testModel));
        }

        [Fact]
        public void PostNewsWithIncorrectData_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var context = this.Context;
            var newsController = new NewsController(context);

            // Act
            newsController.ModelState.AddModelError("Invalid Data", "Invalid Data"); // testing for incorrect data!

            var testModel = this.GetTestData().First();
            var result = newsController.PostNews(this.ProjectToNewsModel(testModel));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteNewsWithIncorrectData_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            newsController.ModelState.AddModelError("Invalid Data", "Invalid Data");

            var incorrectId = 100;
            var result = newsController.DeleteNews(incorrectId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteNewsWithCorrectData_ShouldReturnOkStatusCode()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            var testModel = this.GetTestData().First();
            var result = newsController.DeleteNews(testModel.Id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteNewsWithCorrectData_ShouldRemoveItem()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            var testData = this.GetTestData();

            // Act
            var testModel = testData.First();
            var result = newsController.DeleteNews(testModel.Id);

            var returnedModel = newsController.GetSingleNews(testModel.Id);

            // Assert
            Assert.IsType<NotFoundResult>(returnedModel);
        }

        [Fact]
        public void PutNewsWithIncorrectData_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            var incorrectId = 100;
            var testModel = this.ProjectToNewsModel(this.GetTestData().First());
            var result = newsController.PutNews(incorrectId, testModel);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutNewsWithInvalidModelState_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            var testNews = this.GetTestData().First();
            var testModel = this.ProjectToNewsModel(testNews);
            newsController.ModelState.AddModelError("Invalid Data", "Invalid Data"); // testing for incorrect data!

            var result = newsController.PutNews(testNews.Id, testModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PutNewsWithCorrectData_ShouldReturnOkStatusCode()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            var testNews = this.GetTestData().First();
            var testModel = this.ProjectToNewsModel(testNews);

            var result = newsController.PutNews(testNews.Id, testModel);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void PutNewsWithCorrectData_ShouldUpdateItem()
        {
            // Arrange
            var context = this.Context;
            this.PopulateData(context);
            var newsController = new NewsController(context);

            // Act
            var testNews = this.GetTestData().First();

            var testModel = this.ProjectToNewsModel(testNews);
            testModel.Title = "Updated Title";
            testModel.Content = "Updated Content";
            testModel.PublishDate = DateTime.UtcNow;

            var result = newsController.PutNews(testNews.Id, testModel);
            var returnedModel = (newsController.GetSingleNews(testNews.Id) as OkObjectResult)
                                .Value as News;

            // Assert
            Assert.NotNull(returnedModel);
            Assert.Equal(testNews.Id, returnedModel.Id);
            Assert.Equal(testModel.Title, returnedModel.Title);
            Assert.Equal(testModel.Content, returnedModel.Content);
            Assert.Equal(testModel.PublishDate, returnedModel.PublishDate);
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

        private IEnumerable<News> GetTestData()
            => new List<News>
            {
                new News{Id = 1, Title = "Title1", Content = "Content1", PublishDate = DateTime.ParseExact("2017/12/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) },
                new News{Id = 2, Title = "Title2", Content = "Content2", PublishDate = DateTime.ParseExact("2017/11/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) },
                new News{Id = 3, Title = "Title3", Content = "Content3", PublishDate = DateTime.ParseExact("2017/10/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) },
                new News{Id = 4, Title = "Title4", Content = "Content4", PublishDate = DateTime.ParseExact("2017/03/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) },
                new News{Id = 5, Title = "Title5", Content = "Content5", PublishDate = DateTime.ParseExact("2015/12/09", "yyyy/MM/dd", CultureInfo.InvariantCulture) }
            };

        private void PopulateData(NewsDbContext context)
        {
            context.News.AddRange(this.GetTestData());
            context.SaveChanges();
        }
    }
}
