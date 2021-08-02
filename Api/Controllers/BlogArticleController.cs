using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Api.DTOs;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/articulos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlogArticleController : Controller
    {
        private readonly Service_Blog_Article _serviceBlogArticle;

        public BlogArticleController(Service_Blog_Article serviceBlogArticle)
        {
            _serviceBlogArticle = serviceBlogArticle;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<DTO_Blog_Article>>> GetBlogArticles([FromHeader] string web_link)
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

        [HttpGet("article_link")]
        public async Task<ActionResult<List<DTO_Blog_Article_Comment>>> getArticleComments([FromHeader] string articleLink)
        {
            if (await _serviceBlogArticle.articleExist(articleLink))
            {
                List<DTO_Blog_Article_Comment> articles = await _serviceBlogArticle.getCommentsByArticleID(articleLink);
                return articles;
            }
            return BadRequest(Errors.unknown_error);
        }
    }
}