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
    public class ServiceWebUser:Service
    {
        public ServiceWebUser(ApiContext context, IConfiguration configuration):base(context, configuration)
        {
        }

        public ServiceWebUser()
        {
            
        }

        public async Task<WebUser> createUser(DtoWebUser userDTO)
        {
            WebUser user = Utls.mapper.Map<WebUser>(userDTO);
            var user_created = await _context.DbWebUser.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;        
        }


        public async Task<bool> userExist(DtoWebUser user)
        {
            bool exist = false;
            if (user != null)
            {
                WebUser user_exist = await _context.DbWebUser.AsQueryable().Where(e => e.Email== user.Email).SingleAsync();
                if (user_exist != null)
                {
                    exist = true;
                    return exist;
                }
            }
            return exist;
        }

        public DtoWebAuthAnswer createUserJWT(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("Email", email)
                };
                DateTime expirationDate = DateTime.Now.AddDays(1);

                string tokenJWT = TokenConstructor(claims, expirationDate);

                DtoWebAuthAnswer answer = new DtoWebAuthAnswer()
                {
                    Token = tokenJWT,
                    Expiration = expirationDate
                };

                return answer;
            }

            throw new Exception(Errors.UnknownError);
            return null;
        }

        public async Task<List<DtoWebUser>> get_DTO_Users_By_BlogLink(string blog_link)
        {
            List<WebUser> users = await getUsersByBlogLink(blog_link);
            
            return Utls.mapper.Map<List<DtoWebUser>>(users);
        }
        
        public async Task<WebUser> userLogin(DtoLogin creds)
        {
            Console.WriteLine(_configuration["ApiConnection_Prod"]);
            WebUser user_login = null;
            if (creds != null)
            {
                
                WebUser aux_user_login = await _context.DbWebUser.AsQueryable().Where(e => e.Email == creds.username).SingleAsync();
                bool passverify = BCrypt.Net.BCrypt.Verify(creds.password, aux_user_login.PasswordHash);
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