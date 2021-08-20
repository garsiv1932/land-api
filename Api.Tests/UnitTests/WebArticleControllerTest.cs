using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Context;
using Api.Controllers;
using Api.DTOs;
using Api.Model;
using Api.SRVs;
using Api.Tests;
using Api.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Tests.UnitTests
{
    [TestClass]
    public class WebArticleControllerTest
    {
       [TestMethod]
        public async Task GetWebArticlesOk()
        {
            WebArticleTestObj testObj = new WebArticleTestObj();
            
            //Execution
            var result = await testObj.ArticleController.GetWebArticles("https://www.llorachdevs.com"); 
            
            //Verification
            Assert.That.Equals(typeof(List<DtoWebArticle>));
        }

        [TestMethod]
        public async Task GetWebArticlesError()
        {
            WebArticleTestObj testObj = new WebArticleTestObj();
            
            //Execution
            var response = await testObj.ArticleController.GetWebArticles("xxxxxx");
            var result = response.Result as StatusCodeResult;
            
            //Result
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task GetWebArticlesEmpty()
        {
            WebArticleTestObj testObj = new WebArticleTestObj();

            var response = await testObj.ArticleController.GetWebArticles("");
            var result = response.Result as BadRequestObjectResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task getAmountArticlesOk()
        {
            WebArticleTestObj testObj = new WebArticleTestObj();
        
            var response = await testObj.ArticleController.getAmountArticles("https://www.llorachdevs.com");
            var result = response.Result as OkObjectResult;
        
            Assert.AreEqual(200, result.StatusCode );
        }

        [TestMethod]
        public async Task getAmountArticlesError()
        {
            WebArticleTestObj testObj = new WebArticleTestObj();

            var response = await testObj.ArticleController.getAmountArticles("xxxxx");
            var result = response.Result as StatusCodeResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task getAmountArticlesEmpty()
        {
            WebArticleTestObj testObj = new WebArticleTestObj();

            var response = await testObj.ArticleController.getAmountArticles("");

            var result = response.Result as BadRequestObjectResult;
            
            Assert.AreEqual(400, result.StatusCode);
        }



    }
    
    class WebArticleTestObj:TestDb
    {
        public ApiContext Context { get; set; }
        public WebArticleController ArticleController { get; set; }

        public WebArticleTestObj()
        {
            var dbName = Guid.NewGuid().ToString();
            ApiContext ContextOne = ContextConstructor(dbName);
        
            Web pabloWeb = new Web("https://www.llorachdevs.com");
            WebUserRole role = new("Admin");
            WebUser user = new WebUser("Pablo", "Enrique", "Llorach", "Paz", "", "llorach.pablo@llorachdevs.com", new DateTime(1984, 11, 03),"xxxx", "+59891211845", pabloWeb, role);
            WebArticle articleOne = new WebArticle("linkOne", "imageOne", user, "tittleOne");
            WebArticle articleTwo = new WebArticle("linkTwo", "imageTwo", user, "tittleTwo");
        
            ContextOne.Db_Webs.Add(pabloWeb);
            ContextOne.DbUserRole.Add(role);
            ContextOne.DbWebUser.Add(user);
            ContextOne.DbArticles.Add(articleOne);
            ContextOne.DbArticles.Add(articleTwo);
            ContextOne.SaveChanges();
        
            Context = ContextConstructor(dbName);
        
            IConfiguration configMock = new ConfigurationMock();
        
            ServiceArticle serviceArticle = new ServiceArticle(Context, configMock);
            ServiceLogs serviceLogs = new ServiceLogs(Context, configMock);
            WebArticleController articleController = new WebArticleController(serviceArticle, serviceLogs);
        
            ArticleController = new WebArticleController(serviceArticle, serviceLogs);
        }
    }
}

