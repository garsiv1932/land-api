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
        private readonly Service_Web_User _webUserService;
        private readonly IConfiguration _configuration;

        public WebUserController( Service_Web_User webUserService, IConfiguration configuration)
        {
            _webUserService = webUserService;
            _configuration = configuration;
        }
        
        
        [HttpPost("register")]
        public async Task<ActionResult<DTO_Web_AuthAnswer>> Register([FromHeader] DTO_Web_User userToCreate)
        {
            if (userToCreate != null)
            {
                using (_webUserService)
                {
                    if (_webUserService.passwordValid(userToCreate.Password))
                    {
                        if (!await _webUserService.userExist(userToCreate))
                        {
                            Web_User user_created = await _webUserService.createUser(userToCreate);

                            if (user_created != null)
                            {
                                try
                                {
                                    DTO_Web_AuthAnswer answer = _webUserService.createUserJWT(userToCreate.Email);
                                    return Ok(answer);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    return BadRequest(e.Message);
                                }

                            }
                            return new BadRequestObjectResult(Errors.unknown_error);
                        }
                        return new BadRequestObjectResult(Errors.emailExist(userToCreate.Email));
                    }
                }
            }
            return new BadRequestObjectResult(Errors.unknown_error);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<DTO_Web_AuthAnswer>> Login([FromBody]DTO_Login creds)
        {
            if (creds != null)
            {
                try
                {
                    Web_User userlogin = await _webUserService.userLogin(creds);

                    if ( userlogin != null)
                    {
                        DTO_Web_AuthAnswer answer = _webUserService.createUserJWT(userlogin.Email);
                        return Ok(answer);
                    }

                    return new BadRequestObjectResult(Errors.user_not_authorized);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
;
            }

            return new BadRequestObjectResult(Errors.unknown_error);
        }

        [HttpGet("{blogLink}")]
        public async Task<ActionResult<List<DTO_Web_User>>> getUsersByBlogLink(string blogLink)
        {
            if (!string.IsNullOrWhiteSpace(blogLink))
            {
                return await _webUserService.get_DTO_Users_By_BlogLink(blogLink);
            }
            return new BadRequestObjectResult(Errors.unknown_error);
        }
    }
}