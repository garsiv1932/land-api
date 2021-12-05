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
        private readonly ServiceArticle _serviceArticle;
        private readonly ServiceLogs _serviceLogs;

        public WebArticleController(ServiceArticle serviceArticle, ServiceLogs serviceLogs)
        {
            _serviceArticle = serviceArticle;
            _serviceLogs = serviceLogs;
        }
        
        [HttpGet("getArticlesByLink")]
        public async Task<ActionResult<List<DtoWebArticle>>> GetWebArticles([FromHeader] string link)
        {
            ActionResult action = new BadRequestObjectResult(Errors.WrongAttributes);
            if (!string.IsNullOrWhiteSpace(link))
            {
                try
                {
                    List<DtoWebArticle> entries = new();
                    entries = await _serviceArticle.getDtoArticlesByLink(HttpUtility.UrlDecode(link));
                    if (entries == null)
                    {
                        action = new NotFoundResult();
                    }
                    else
                    {
                        action = new OkObjectResult(entries);                        
                    }
                }
                catch (Exception e)
                {
                    _serviceLogs.AddExcepcion(e.Message, "WebArticleController", "getDtoArticlesByLink","WebArticles");
                    action = BadRequest(e.Message);
                }
            }
            return action;
        }

        [HttpGet("cantidad")]
        public async Task<ActionResult<int>> getAmountArticles([FromHeader] string link)
        {
            ActionResult result = new BadRequestObjectResult(Errors.UnknownError); 
            if (!string.IsNullOrWhiteSpace(link))
            {
                try
                {
                    int cantidadArticulos = await _serviceArticle.cantidadArticulos(link);
                    result = Ok(cantidadArticulos);                
                }
                catch (Exception e)
                {
                    result = new NotFoundResult();
                }
            }
            return result;
        }

        [HttpGet("article_link")]
        public async Task<ActionResult<List<DtoWebArticleComment>>> getArticleComments([FromHeader] string articleLink)
        {
            if (await _serviceArticle.articleExist(articleLink))
            {
                List<DtoWebArticleComment> Comments = await _serviceArticle.getCommentsByArticleID(articleLink);
                return Comments;
            }
            return BadRequest(Errors.ElementNotFound);
        }
    }
}