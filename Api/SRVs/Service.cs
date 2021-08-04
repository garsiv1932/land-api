using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Api.Context;
using Api.Model;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Services.SRVs
{
    public abstract class Service : IDisposable, IService
    {
        private readonly IConfiguration _configuration;

        protected ApiContext _context { get; }

        public Service(ApiContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public Service()
        {

        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public async Task<List<Web_User>> getUsersByBlogLink([FromHeader] string blog_link)
        {
            
            if (!string.IsNullOrWhiteSpace(blog_link))
            {
                string decodedUrl = HttpUtility.UrlDecode(blog_link);
                try
                {
                    // List<Web_User> users = await _context.Db_Web_User
                    //     .Include(e => e.Web)
                    //     .Include(e => e.Articles)
                    //     .Where(e => e.Site_Link == decodedUrl).ToListAsync();

                    Web web = await _context.Db_Webs
                        .Include(e => e.users)
                        .ThenInclude(e => e.Articles)
                        .ThenInclude(e => e.Comments)
                        .Where(e => e.Site_Link == blog_link)
                        .SingleOrDefaultAsync();
                    return web.users;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
            throw new Exception(Errors.unknown_error);
        }
    
        public string TokenConstructor(List<Claim> claimList, DateTime expiration )
        {
            string answer = string.Empty;
            if (claimList != null && expiration > DateTime.Today)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken securityToken = 
                    new JwtSecurityToken(issuer:null, audience: null, claims:claimList, expires: expiration, signingCredentials:creds);

                answer = new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            return answer;
        }
    }
}