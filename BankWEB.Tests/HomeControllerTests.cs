using BankWEB.Controllers;
using BankWEB.Interfaces;
using BankWEB.Models;
using BankWEB.Models.Enums;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BankWEB.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        Client TestClient = new Client { Id = 1, Status = Status.Individual, Username = "TestClient" };

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext) { }

        [ClassCleanup]
        public static void ClassCleanup() { }


        public HomeController GetCustomController(Mock<IBankData> bankMock, MemoryCache cache)
        {
            // Identity.Name
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, TestClient.Username)
            };
            var identity = new ClaimsIdentity(claims, "Test");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var mockPrincipal = new Mock<IPrincipal>();
            mockPrincipal.Setup(obj => obj.Identity).Returns(identity);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(obj => obj.User).Returns(claimsPrincipal);

            // HomeController
            var controller = new HomeController(bankMock.Object, cache);
            controller.ControllerContext = new ControllerContext(new ActionContext(mockHttpContext.Object, new RouteData(), new ControllerActionDescriptor()));
            return controller;
        }

        [TestMethod]
        public void IndexGet_ClientReturnedFromRepo()
        {
            // Arrange
            var bankMock = new Mock<IBankData>();
            bankMock.Setup(obj => obj.GetClientByUsernameAsync(It.IsAny<string>())).ReturnsAsync(TestClient);

            var cache = new MemoryCache(new MemoryCacheOptions());

            TestClient.Username = "ClientNotInCache";

            var controller = GetCustomController(bankMock, cache);


            // Art
            var viewActual = controller.Index().Result as ViewResult;
            cache.TryGetValue(TestClient.Username, out Client? cacheClient);


            // Assert
            Assert.AreEqual(TestClient.Username, ((Client)viewActual.Model).Username);
            Assert.AreEqual(TestClient, cacheClient);
        }

        [TestMethod]
        public void IndexGet_ClientReturnedFromCache()
        {
            // Arrange
            var bankMock = new Mock<IBankData>();
            bankMock.Setup(obj => obj.GetClientByUsernameAsync(It.IsAny<string>())).ReturnsAsync(new Client { Username = "ErrorTest" });

            TestClient.Username = "ClientInCache";

            var cache = new MemoryCache(new MemoryCacheOptions());
            cache.Set(TestClient.Username, TestClient, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)));

            var controller = GetCustomController(bankMock, cache);

            // Art
            var viewActual = controller.Index().Result as ViewResult;
            cache.TryGetValue(TestClient.Username, out Client? cacheClient);

            // Assert
            Assert.AreEqual(TestClient.Username, cacheClient!.Username);
        }

        [TestMethod]
        [Ignore]
        public async Task IntegrationTestExample()
        {
            // Arrange
            await using var applicationFactory = new WebApplicationFactory<Program>();
            var HttpClient = applicationFactory.CreateClient();

            // Art
            // Assert
        }
    }
}