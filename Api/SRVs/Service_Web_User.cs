using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Model;
using Api.Context;
using Api.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.SRVs
{
    public class Service_Web_User:Service
    {
        public Service_Web_User(ApiContext context, IConfiguration configuration):base(context, configuration)
        {
        }

        public Service_Web_User()
        {
            
        }

        public async Task<Web_User> createUser(DTO_Web_User userDTO)
        {
            Web_User user = Utls.mapper.Map<Web_User>(userDTO);
            var user_created = await _context.Db_Web_User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;        
        }


        public async Task<bool> userExist(DTO_Web_User user)
        {
            bool exist = false;
            if (user != null)
            {
                Web_User user_exist = await _context.Db_Web_User.AsQueryable().Where(e => e.Email== user.Email).SingleAsync();
                if (user_exist != null)
                {
                    exist = true;
                    return exist;
                }
            }
            return exist;
        }

        public DTO_Web_AuthAnswer createUserJWT(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("Email", email)
                };
                DateTime expirationDate = DateTime.Now.AddDays(1);

                string tokenJWT = TokenConstructor(claims, expirationDate);

                DTO_Web_AuthAnswer answer = new DTO_Web_AuthAnswer()
                {
                    Token = tokenJWT,
                    Expiration = expirationDate
                };

                return answer;
            }

            throw new Exception(Errors.unknown_error);
            return null;
        }

        public async Task<List<DTO_Web_User>> get_DTO_Users_By_BlogLink(string blog_link)
        {
            List<Web_User> users = await getUsersByBlogLink(blog_link);
            
            return Utls.mapper.Map<List<DTO_Web_User>>(users);
        }
        
        public async Task<Web_User> userLogin(DTO_Login creds)
        {
            Console.WriteLine(_configuration["ApiConnection_Prod"]);
            Web_User user_login = null;
            if (creds != null)
            {
                
                Web_User aux_user_login = await _context.Db_Web_User.AsQueryable().Where(e => e.Email == creds.username).SingleAsync();
                bool passverify = BCrypt.Net.BCrypt.Verify(creds.password, aux_user_login.Password_Hash);
                if (aux_user_login != null && passverify)
                {
                    user_login = aux_user_login;
                }
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