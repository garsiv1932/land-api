using System;
using System.Threading.Tasks;
using Api.DTOs;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
// using Swashbuckle.AspNetCore.Annotations;

//using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Api.Controllers
{
    [Route("api/web")]
    [ApiExplorerSettings(GroupName = "Service - Web")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WebController : Controller
    {
        private readonly ServiceWeb _serviceWeb;


        public WebController(ServiceWeb serviceWeb)
        {
            _serviceWeb = serviceWeb;
        }

        [HttpPost("{name}/{address_link}")]
        public IActionResult AddBlog(string name, string address_link)
        {
            //To Do:
            //validar si no existe el blog por el link
            return Ok();
        }

        [HttpGet("web_link")]
        public async Task<ActionResult<DtoWeb>> GetBlogByLink([FromHeader] string web_link)
        {
            DtoWeb entries = new();
            entries = await _serviceWeb.getBlogsByLink(web_link);
            if (entries == null)
            {
                return NotFound();
            }
            return entries;
        }



        [HttpGet]
        public async Task<ActionResult<DtoWeb>> GetBlogs()
        {
            DtoWeb entries = new();
            entries = await _serviceWeb.getBlogs();
            if (entries == null)
            {
                return NotFound();
            }
            return entries;
        }

        [HttpPost]
        [Route("jwt-site")]
        public async Task<ActionResult<DtoWebAuthAnswer>> CreateNewWebJWT([FromHeader] string web_link)
        {
            if (!string.IsNullOrWhiteSpace(web_link))
            {
                try
                {
                    DtoWebAuthAnswer authAnswer = await _serviceWeb.AddJwtToSite(web_link);
                    return authAnswer;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return BadRequest(e.Message);
                }

            }
            return BadRequest(Errors.UnknownError);
        }
    }
}