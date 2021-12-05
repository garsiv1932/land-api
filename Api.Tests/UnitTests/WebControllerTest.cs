using System.Threading.Tasks;
using Api.Controllers;
using Api.Tests.TestObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Tests.UnitTests
{
    [TestClass]
    public class WebControllerTest
    {
        private WebController _webController = ControllerFactory.createWebController();

        [TestMethod]
        public async Task GetWebDataError()
        {
            var response = await _webController.GetWebData("xxxxx");
            var result = response.Result as NotFoundResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task GetWebDataOk()
        {
            var response = await _webController.GetWebData("https://www.llorachdevs.com");
            var result = response.Result as OkObjectResult;
            
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task GetWebDataEmpty()
        {
            var response = await _webController.GetWebData("");
            var result = response.Result as BadRequestObjectResult;
            
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateNewWebJwtOk()
        {
            var response = await _webController.CreateNewWebJWT("https://www.llorachdevs.com");
            var result = response.Result as OkObjectResult;
            
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateNewWebJwtError()
        {
            var response = await _webController.CreateNewWebJWT("xxxxx");
            var result = response.Result as NotFoundResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateNewWebJwtEmpty()
        {
            var response = await _webController.CreateNewWebJWT("");
            var result = response.Result as BadRequestObjectResult;
            
            Assert.AreEqual(400, result.StatusCode);
        }


    }
}