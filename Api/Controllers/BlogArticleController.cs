using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Api.DTOs;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/articulos")]
    public class BlogArticleController : Controller
    {
        private readonly Service_Blog_Article _serviceBlogArticle;

        public BlogArticleController(Service_Blog_Article serviceBlogArticle)
        {
            _serviceBlogArticle = serviceBlogArticle;
        }
        
        [HttpGet("{web_link}")]
        public async Task<ActionResult<List<DTO_Blog_Article>>> GetBlogArticles(string web_link)
            {
            if (!string.IsNullOrWhiteSpace(web_link))
            {
                List<DTO_Blog_Article> entries = new();

                using (_serviceBlogArticle)
                {
                    entries = await _serviceBlogArticle.get_DTO_Articles_ByLink(HttpUtility.UrlDecode(web_link));
                }

                if (entries == null)
                {
                    return NotFound();
                }

                return entries;
            }

            return BadRequest(Errors.wrong_attributes);
        }

        [HttpGet("{articleID:long}")]
        public async Task<ActionResult<List<DTO_Blog_Article_Comment>>> getArticleComments(DateTime articleID)
        {
            if (await _serviceBlogArticle.articleExist(articleID))
            {
                List<DTO_Blog_Article_Comment> articles = await _serviceBlogArticle.getCommentsByArticleID(articleID);
                return articles;
            }
            return BadRequest(Errors.unknown_error);
        }
    }
}