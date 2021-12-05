using System;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Model;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/web")]
    [ApiExplorerSettings(GroupName = "Service - Web")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WebController : Controller
    {
        private readonly ServiceWeb _serviceWeb;
        private readonly ServiceLogs _serviceLogs;


        public WebController(ServiceWeb serviceWeb, ServiceLogs serviceLogs)
        {
            _serviceWeb = serviceWeb;
            _serviceLogs = serviceLogs;
        }

        [HttpPost("{name}/{address_link}")]
        public IActionResult AddBlog(string name, string address_link)
        {
            //To Do:
            //validar si no existe el blog por el link
            return Ok();
        }

        [HttpGet("web_link")]
        public async Task<ActionResult<DtoWeb>> GetWebData([FromHeader] string web_link)
        {
            ActionResult action = new BadRequestObjectResult(Errors.UnknownError);
            if (!string.IsNullOrWhiteSpace(web_link))
            {
                DtoWeb webData = new();
                 DtoWeb searchedWeb =  await _serviceWeb.getDtoWebByLink(web_link);
                 
                if (webData == null)
                {
                    action= NotFound();
                }
                else
                {
                    action = Ok(webData);
                }
            }
            return action;
        }



        [HttpGet]
        public async Task<ActionResult<DtoWeb>> GetWebs()
        {
            ActionResult action = new BadRequestResult();
            DtoWeb entries = new();
            entries = await _serviceWeb.getBlogs();
            if (entries == null)
            {
                action = NotFound();
            }
            else
            {
                action = new OkObjectResult(entries);
            }
            return entries;
        }

        [HttpPost]
        [Route("jwt-site")]
        public async Task<ActionResult<AuthAnswer>> CreateNewWebJWT([FromHeader] string web_link)
        {
            ActionResult action = new BadRequestObjectResult(Errors.UnknownError);
            if (!string.IsNullOrWhiteSpace(web_link))
            {
                try
                {
                    AuthAnswer authAnswer = await _serviceWeb.AddJwtToSite(web_link);
                    if (authAnswer != null)
                    {
                        action = new OkObjectResult(authAnswer);                        
                    }
                    else
                    {
                        action = new NotFoundResult();
                    }
                }
                catch (Exception e)
                {
                    action = BadRequest(e.Message);
                }
            }

            return action;
        }
    }
}