using System;
using Api.Context;
using Api.Controllers;
using Api.Model;
using Api.SRVs;
using Api.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Api.Tests.TestObjects
{
    public static class ControllerFactory
    {
        private static ApiContext _context { get; set; }
        private static IConfiguration _configMock { get; set; }
        
        static ControllerFactory()
        {
            var dbName = Guid.NewGuid().ToString();
            ApiContext contextOne = preloadedContext(dbName);
            _context = ContextConstructor(dbName);
            
            _configMock = new ConfigurationMock();
        }

        static ApiContext ContextConstructor( string dbName)
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(dbName).Options;
            var dbContext = new ApiContext(options);

            return dbContext;
        }

        static ApiContext preloadedContext(string contextName)
        {
            ApiContext context = ContextConstructor(contextName);
            Web pabloWeb = new Web("https://www.llorachdevs.com");
            WebUserRole role = new("Admin");
            WebUser user = new WebUser("Pablo", "Enrique", "Llorach", "Paz", "", "llorach.pablo@llorachdevs.com", new DateTime(1984, 11, 03),"xxxx", "+59891211845", pabloWeb, role);
            WebArticle articleOne = new WebArticle("linkOne", "imageOne", user, "tittleOne");
            WebArticle articleTwo = new WebArticle("linkTwo", "imageTwo", user, "tittleTwo");
        
            context.Db_Webs.Add(pabloWeb);
            context.DbUserRole.Add(role);
            context.DbWebUser.Add(user);
            context.DbArticles.Add(articleOne);
            context.DbArticles.Add(articleTwo);
            context.SaveChanges();

            return context;
        }

        

        public static WebArticleController createArticleController()
        {

            ServiceLogs serviceLogs = new ServiceLogs(_context, _configMock);
            
            ServiceArticle serviceArticle = new ServiceArticle(_context, _configMock);
            WebArticleController articleCont = new WebArticleController(serviceArticle, serviceLogs);
            
            return articleCont;
        }
        
        public static WebController createWebController()
        {
            ServiceLogs serviceLogs = new ServiceLogs(_context, _configMock);
            ServiceWeb serviceArticle = new ServiceWeb(_context, _configMock);
            
            WebController articleCont = new WebController(serviceArticle, serviceLogs);
            
            return articleCont;
        }

        public static WebUserController creatorWebUserController()
        {
            ServiceLogs serviceLogs = new ServiceLogs(_context, _configMock);
            ServiceWeb serviceWeb = new ServiceWeb(_context, _configMock);
            ServiceWebUser serviceUser = new ServiceWebUser(_context, _configMock,serviceWeb);
            WebUserController userController = new WebUserController(serviceUser, serviceLogs);

            return userController;
        }

        public static WebVisitController createWebVisitController()
        {
            ServiceLogs serviceLogs = new ServiceLogs(_context, _configMock);
            ServiceVisit serviceVisit = new ServiceVisit(_context,_configMock );
            WebVisitController webVisitController = new WebVisitController(serviceVisit, serviceLogs);
            return webVisitController;
        }
    }
}