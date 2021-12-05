using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Api.DTOs;
using Api.Tests.TestObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Tests.UnitTests
{
    [TestClass]
    public class WebArticleControllerTest
    {
        private readonly WebArticleController _articleController = ControllerFactory.createArticleController();
        
       [TestMethod]
        public async Task GetWebArticlesOk()
        {
            //Execution
            var result = await _articleController.GetWebArticles("https://www.llorachdevs.com"); 
            
            //Verification
            Assert.That.Equals(typeof(List<DtoWebArticle>));
        }

        [TestMethod]
        public async Task GetWebArticlesError()
        {
            //Execution
            var response = await _articleController.GetWebArticles("xxxxxx");
            var result = response.Result as StatusCodeResult;
            
            //Result
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task GetWebArticlesEmpty()
        {
            var response = await _articleController.GetWebArticles("");
            var result = response.Result as BadRequestObjectResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task getAmountArticlesOk()
        {
            var response = await _articleController.getAmountArticles("https://www.llorachdevs.com");
            var result = response.Result as OkObjectResult;
        
            Assert.AreEqual(200, result.StatusCode );
        }

        [TestMethod]
        public async Task getAmountArticlesError()
        {
            var response = await _articleController.getAmountArticles("xxxxx");
            var result = response.Result as StatusCodeResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task getAmountArticlesEmpty()
        {
            var response = await _articleController.getAmountArticles("");

            var result = response.Result as BadRequestObjectResult;
            
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}

