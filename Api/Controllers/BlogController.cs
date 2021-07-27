using System.Threading.Tasks;
using Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.SRVs;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/blogs")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlogController : Controller
    {
        private readonly Service_Blog _serviceBlog;

        public BlogController(Service_Blog serviceBlog)
        {
            _serviceBlog = serviceBlog;
        }
 
        [HttpPost("{name}/{address_link}")]
        public IActionResult AddBlog(string name, string address_link)
        {
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
    }
}