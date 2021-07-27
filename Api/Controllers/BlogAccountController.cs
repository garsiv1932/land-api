using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Model;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class BlogAccountController
    {
        private readonly Service_Blog_User _blogUserService;
        private readonly IConfiguration _configuration;

        public BlogAccountController( Service_Blog_User blogUserService, IConfiguration configuration)
        {
            _blogUserService = blogUserService;
            _configuration = configuration;
        }
        
        
        [HttpPost("Register")]
        public async Task<ActionResult<DTO_Blog_User_AuthAnswer>> Register(DTO_Blog_User userToCreate)
        {
            if (userToCreate != null)
            {
                using (_blogUserService)
                {
                    if (_blogUserService.passwordValid(userToCreate.Password))
                    {
                        if (!await _blogUserService.userExist(userToCreate))
                        {
                            Blog_User user_created = await _blogUserService.createUser(userToCreate);

                            if (user_created != null)
                            {
                                return TokenConstructor(userToCreate);
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
        public async Task<ActionResult<DTO_Blog_User_AuthAnswer>> Login(DTO_Blog_User user)
        {
            if (user != null)
            {
                Blog_User userlogin = await _blogUserService.userLogin(user);

                if ( userlogin != null)
                {
                    return TokenConstructor(user);
                }

                return new BadRequestObjectResult(Errors.user_not_authorized);
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
        

        private DTO_Blog_User_AuthAnswer TokenConstructor(DTO_Blog_User user_credentials)
        {
            var claim = new List<Claim>()
            {
                new Claim("Email", user_credentials.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = 
                new JwtSecurityToken(issuer:null, audience: null, claims:claim, expires: expiration, signingCredentials:creds);

            return new DTO_Blog_User_AuthAnswer()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
        }
    }
}