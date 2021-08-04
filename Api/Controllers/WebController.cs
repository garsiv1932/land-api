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
        private readonly Service_Web _serviceWeb;
        private readonly IConfiguration _configuration;


        public WebController(Service_Web serviceWeb, IConfiguration configuration)
        {
            _serviceWeb = serviceWeb;
            _configuration = configuration;
        }

        [HttpPost("{name}/{address_link}")]
        public IActionResult AddBlog(string name, string address_link)
        {
            //To Do:
            //validar si no existe el blog por el link
            return Ok();
        }

        [HttpGet("web_link")]
        public async Task<ActionResult<DTO_Web>> GetBlogByLink([FromHeader] string web_link)
        {
            DTO_Web entries = new();

            using (Service_Web serviceWeb = _serviceWeb)
            {
                entries = await serviceWeb.getBlogsByLink(web_link);
            }

            if (entries == null)
            {
                return NotFound();
            }

            return entries;
        }



        [HttpGet]
        public async Task<ActionResult<DTO_Web>> GetBlogs()
        {
            DTO_Web entries = new();

            using (Service_Web serviceWeb = _serviceWeb)
            {
                entries = await serviceWeb.getBlogs();
            }

            if (entries == null)
            {
                return NotFound();
            }

            return entries;
        }

        [HttpPost]
        [Route("jwt-site")]
        public async Task<ActionResult<DTO_Web_AuthAnswer>> CreateNewWebJWT([FromHeader] string web_link)
        {
            if (string.IsNullOrWhiteSpace(web_link))
            {
                using (_serviceWeb)
                {
                    try
                    {
                        DTO_Web_AuthAnswer authAnswer = await _serviceWeb.AddJwtToSite(web_link);
                        return authAnswer;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return BadRequest(e.Message);
                    }
                }
            }
            return BadRequest(Errors.unknown_error);
        }
    }
}