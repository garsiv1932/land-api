using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Model;
using AutoMapper;
using Api.Context;
using Api.Utilities;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Services.SRVs;

namespace Api.SRVs
{
    public class Service_Blog_User:Service
    {
        private readonly IMapper _mapper;

        public Service_Blog_User(ApiContext context, IMapper mapper, IConfiguration configuration):base(context, configuration)
        {
            _mapper = mapper;
        }

        public async Task<Web_User> createUser(DTO_Blog_User userDTO)
        {
            Web_User user = _mapper.Map<Web_User>(userDTO);
            var user_created = await _context.Db_Blog_User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;        
        }


        public async Task<bool> userExist(DTO_Blog_User user)
        {
            bool exist = false;
            if (user != null)
            {
                Web_User user_exist = await _context.Db_Blog_User.AsQueryable().Where(e => e.Email== user.Email).SingleAsync();
                if (user_exist != null)
                {
                    exist = true;
                    return exist;
                }
            }
            return exist;
        }

        public DTO_Blog_AuthAnswer createUserJWT(string email, string key)
        {
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(key))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("Email", email)
                };
                DateTime expirationDate = DateTime.Now.AddDays(1);

                JwtSecurityToken tokenJWT = TokenConstructor(claims, key, expirationDate);

                DTO_Blog_AuthAnswer answer = new DTO_Blog_AuthAnswer()
                {
                    Token = tokenJWT,
                    Expiration = expirationDate
                };

                return answer;
            }

            throw new Exception(Errors.unknown_error);
            return null;
        }

        public async Task<List<DTO_Blog_User>> get_DTO_Users_By_BlogLink(string blog_link)
        {
            List<Web_User> users = await getUsersByBlogLink(blog_link);
            
            return Utls.mapper.Map<List<DTO_Blog_User>>(users);
        }
        
        public async Task<Web_User> userLogin(DTO_Blog_User user)
        {
            Web_User user_login = null;
            if (user != null)
            {
                user_login = await _context.Db_Blog_User.AsQueryable().Where(e => e.Email == user.Email).SingleAsync();
            }
            return user_login;
        }

        public bool passwordValid(string password)
        {
            bool at_least_one_upper = false;
            bool oneNumber = false;
            bool at_least_eight_chars = false;

            if (password.Length > 8)
            {
                at_least_eight_chars = true;
                foreach (char chr in password)
                {
                    string upper = chr.ToString().ToUpper();
                    
                    if (upper == chr.ToString())
                    {
                        at_least_one_upper = true;
                    }

                    int number_check;
                    oneNumber = int.TryParse(chr.ToString(), out number_check);
                }
            }

            return oneNumber && at_least_one_upper && at_least_eight_chars;
        }
    }
}