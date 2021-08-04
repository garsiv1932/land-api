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
    [ApiExplorerSettings(GroupName = "Service - Articulo")]
    [Route("api/articulos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WebArticleController : Controller
    {
        private readonly Service_Web_Article _serviceWebArticle;

        public WebArticleController(Service_Web_Article serviceWebArticle)
        {
            _serviceWebArticle = serviceWebArticle;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<DTO_Web_Article>>> GetWebArticles([FromHeader] string web_link)
            {
            if (!string.IsNullOrWhiteSpace(web_link))
            {
                List<DTO_Web_Article> entries = new();

                using (_serviceWebArticle)
                {
                    entries = await _serviceWebArticle.get_DTO_Articles_ByLink(HttpUtility.UrlDecode(web_link));
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
        public async Task<ActionResult<List<DTO_Web_Article_Comment>>> getArticleComments([FromHeader] string articleLink)
        {
            if (await _serviceWebArticle.articleExist(articleLink))
            {
                List<DTO_Web_Article_Comment> Comments = await _serviceWebArticle.getCommentsByArticleID(articleLink);
                return Comments;
            }
            return BadRequest(Errors.element_not_found);
        }
    }
}