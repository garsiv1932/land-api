using System.Threading.Tasks;
using Api.Controllers;
using Api.DTOs;
using Api.Tests.TestObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Tests.UnitTests
{
 
    
    [TestClass]
    public class WebUserControllerTest
    {
        private WebUserController _controller = ControllerFactory.creatorWebUserController();
        
        [TestMethod]
        public async Task RegisterTestOk()
        {
            DtoWebUser user = new DtoWebUser();
            user.Email = "x@x";
            user.FirstName = "";
            user.FirstLastName = "";
            user.SecondLastName = "";
            user.Password = "13aBr2009";
            user.WebLink = "https://www.llorachdevs.com";
            
            var response = await _controller.Register(user);
            var result = response.Result as OkObjectResult;
            
            Assert.AreEqual(200,result.StatusCode);
        }

        public async Task RegisterError()
        {
            DtoWebUser user = new DtoWebUser();
            user.Email = "x@x";
            user.FirstName = "";
            user.FirstLastName = "";
            user.SecondLastName = "";
            user.Password = "13aBr2009";
            user.WebLink = "https://www.llorachdevs.com";
        }

        public async Task RegisterEmpty()
        {
            DtoWebUser user = new DtoWebUser();
            user.Email = "x@x";
            user.FirstName = "";
            user.FirstLastName = "";
            user.SecondLastName = "";
            user.Password = "13aBr2009";
            user.WebLink = "https://www.llorachdevs.com";
        }
    }
    
    
}