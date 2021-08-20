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
        private readonly IConfiguration _configuration;

        public WebUserController(ServiceWebUser webUserService, IConfiguration configuration)
        {
            _webUserService = webUserService;
            _configuration = configuration;
        }
        
        
        [HttpPost("register")]
        public async Task<ActionResult<DtoWebAuthAnswer>> Register([FromHeader] DtoWebUser userToCreate)
        {
            if (userToCreate != null)
            {
                if (_webUserService.passwordValid(userToCreate.Password))
                {
                    if (!await _webUserService.userExist(userToCreate))
                    {
                        WebUser user_created = await _webUserService.createUser(userToCreate);

                        if (user_created != null)
                        {
                            try
                            {
                                DtoWebAuthAnswer answer = _webUserService.createUserJWT(userToCreate.Email);
                                return Ok(answer);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                return BadRequest(e.Message);
                            }
                        }
                        return new BadRequestObjectResult(Errors.UnknownError);
                    }
                    return new BadRequestObjectResult(Errors.emailExist(userToCreate.Email));
                }
            }
            return new BadRequestObjectResult(Errors.UnknownError);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<DtoWebAuthAnswer>> Login([FromBody]DtoLogin creds)
        {
            Console.WriteLine(_configuration["ApiConnection_Prod"]);
            if (creds != null)
            {
                try
                {
                    WebUser userlogin = await _webUserService.userLogin(creds);

                    if ( userlogin != null)
                    {
                        DtoWebAuthAnswer answer = _webUserService.createUserJWT(userlogin.Email);
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