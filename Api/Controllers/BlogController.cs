using System;
using System.Threading.Tasks;
using Api.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.SRVs;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/blogs")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlogController : Controller
    {
        private readonly Service_Blog _serviceBlog;
        private readonly IConfiguration _configuration;

        public BlogController(Service_Blog serviceBlog, IConfiguration configuration)
        {
            _serviceBlog = serviceBlog;
            _configuration = configuration;
        }
 
        [HttpPost("{name}/{address_link}")]
        public IActionResult AddBlog(string name, string address_link)
        {
            //To Do:
            //validar si no existe el blog por el link
            return Ok();
        }
        
        [HttpGet("{web_link}")]
        public async Task<ActionResult<DTO_Blog>> GetBlogByLink(string web_link)
        {
            DTO_Blog entries = new();
        
            using (Service_Blog serviceBlog = _serviceBlog)
            {
                entries = await serviceBlog.getBlogsByLink(web_link);
            }
        
            if (entries == null)
            {
                return NotFound();
            }
        
            return entries;
        }

        

        [HttpGet]
        public async Task<ActionResult<DTO_Blog>> GetBlogs()
        {
            DTO_Blog entries = new();

            using (Service_Blog serviceBlog = _serviceBlog)
            {
                entries = await serviceBlog.getBlogs();
            }

            if (entries == null)
            {
                return NotFound();
            }

            return entries;
        }

        [HttpPost]
        public async Task<ActionResult<DTO_Blog_AuthAnswer>> CreateNewWebJWT([FromHeader] string web_link)
        {
            try
            {
                using (_serviceBlog)
                {
                    try
                    {
                        DTO_Blog_AuthAnswer authAnswer = await _serviceBlog.AddJwtToSite(web_link, _configuration["JwtKey"]);
                        return authAnswer;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return BadRequest(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        

    }
}