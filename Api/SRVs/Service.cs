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
using Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Api.SRVs
{
    public abstract class Service
    {
        public readonly IConfiguration _configuration;

        protected ApiContext _context { get; }

        public Service(ApiContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public Service()
        {

        }

        public async Task<List<WebUser>> getUsersByBlogLink(string blog_link)
        {
            List<WebUser> response = null;
            if (!string.IsNullOrWhiteSpace(blog_link))
            {
                string decodedUrl = HttpUtility.UrlDecode(blog_link);
                try
                {
                    Web web = await _context.Db_Webs
                        .Include(e => e.users)
                        .ThenInclude(e => e.Articles)
                        .Where(e => e.Site_Link == blog_link)
                        .SingleOrDefaultAsync();

                    if (web != null)
                    {
                        response = web.users;                        
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(Errors.UnknownError);
                }
            }
            return response;
        }
    
        public string TokenConstructor(List<Claim> claimList, DateTime expiration )
        {
            if (claimList != null && expiration > DateTime.Now)
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
            throw new Exception(Errors.UnknownError);
        }


        
        public async Task<List<WebUser>> usersByWebLink(string link)
        {
            List<WebUser> users = null;
            if (!string.IsNullOrWhiteSpace(link))
            {
                Web site = await _context.Db_Webs
                    .Include(e => e.users)
                    .ThenInclude(e => e.Articles)
                    .Where(e => e.Site_Link == link)
                    .FirstOrDefaultAsync();

                if (site != null)
                {
                    users = site.users;
                }
            }
            return users;
        }
    }
}