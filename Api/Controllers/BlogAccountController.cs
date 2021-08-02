using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Model;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlogAccountController:ControllerBase
    {
        private readonly Service_Blog_User _blogUserService;
        private readonly IConfiguration _configuration;

        public BlogAccountController( Service_Blog_User blogUserService, IConfiguration configuration)
        {
            _blogUserService = blogUserService;
            _configuration = configuration;
        }
        
        
        [HttpPost("Register")]
        public async Task<ActionResult<DTO_Blog_AuthAnswer>> Register([FromHeader] DTO_Blog_User userToCreate)
        {
            if (userToCreate != null)
            {
                using (_blogUserService)
                {
                    if (_blogUserService.passwordValid(userToCreate.Password))
                    {
                        if (!await _blogUserService.userExist(userToCreate))
                        {
                            Web_User user_created = await _blogUserService.createUser(userToCreate);

                            if (user_created != null)
                            {
                                try
                                {
                                    DTO_Blog_AuthAnswer answer = _blogUserService.createUserJWT(userToCreate.Email, _configuration["JwtKey"]);
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

        [HttpPost("Login")]
        public async Task<ActionResult<DTO_Blog_AuthAnswer>> Login(DTO_Blog_User user)
        {
            if (user != null)
            {
                try
                {
                    Web_User userlogin = await _blogUserService.userLogin(user);

                    if ( userlogin != null)
                    {
                        return Ok(_blogUserService.createUserJWT(user.Email, _configuration["JwtKey"]));
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
        public async Task<ActionResult<List<DTO_Blog_User>>> getUsersByBlogLink(string blogLink)
        {
            if (!string.IsNullOrWhiteSpace(blogLink))
            {
                return await _blogUserService.get_DTO_Users_By_BlogLink(blogLink);
            }
            return new BadRequestObjectResult(Errors.unknown_error);
        }


        




        
    }
}