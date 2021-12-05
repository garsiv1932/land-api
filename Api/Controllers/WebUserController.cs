using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Model;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [ApiExplorerSettings(GroupName = "Service - Users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WebUserController:Controller
    {
        private readonly ServiceWebUser _webUserService;
        private readonly ServiceLogs _serviceLogs;

        public WebUserController(ServiceWebUser webUserService, ServiceLogs serviceLogs)
        {
            _webUserService = webUserService;
            _serviceLogs = serviceLogs;
        }


        [HttpPost("register")]
        public async Task<ActionResult<AuthAnswer>> Register([FromHeader] DtoWebUser webUser)
        {
            ActionResult action = new BadRequestObjectResult(Errors.UnknownError);
            if (webUser != null)
            {
                if (_webUserService.passwordValid(webUser.Password))
                {
                    if (!await _webUserService.userExist(webUser.UserName))
                    {
                        WebUser user_created = await _webUserService.createUser(webUser);

                        if (user_created != null)
                        {
                            try
                            {
                                AuthAnswer answer = _webUserService.createUserJWT(webUser.Email);
                                action = Ok(answer);
                            }
                            catch (Exception e)
                            {
                                action = BadRequest(e.Message);
                            }
                        }
                        else
                        {
                            action = new BadRequestObjectResult(Errors.UnknownError);                            
                        }
                    }
                    else
                    {
                        action = new ConflictObjectResult(Errors.emailExist(webUser.Email));                        
                    }
                }
                else
                {
                    action = new BadRequestObjectResult(Errors.WrongAttributes);
                }
            }
            return action ;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthAnswer>> Login([FromBody]DtoLogin creds)
        {
            if (creds != null)
            {
                try
                {
                    WebUser userlogin = await _webUserService.userLogin(creds);

                    if ( userlogin != null)
                    {
                        AuthAnswer answer = _webUserService.createUserJWT(userlogin.Email);
                        return Ok(answer);
                    }

                    return new BadRequestObjectResult(Errors.UserNotAuthorized);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
;
            }

            return new BadRequestObjectResult(Errors.UnknownError);
        }

        [HttpGet("{blogLink}")]
        public async Task<ActionResult<List<DtoWebUser>>> getUsersByBlogLink(string blogLink)
        {
            if (!string.IsNullOrWhiteSpace(blogLink))
            {
                return await _webUserService.get_DTO_Users_By_BlogLink(blogLink);
            }
            return new BadRequestObjectResult(Errors.UnknownError);
        }
    }
}