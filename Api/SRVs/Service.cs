using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Context;
using Api.DTOs;
using Api.Model;
using Api.SRVs;
using Api.Utilities;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Services.SRVs
{
    public abstract class Service : IDisposable,IService
    {
        protected readonly IConfiguration _configuration;

        protected ApiContext _context { get; }

        public Service(ApiContext context, IConfiguration configuration)
        {
            _configuration = configuration;
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
        
        public async Task<List<Web_User>> getUsersByBlogLink(string blog_link)
            {
            Web web = await _context.Db_Blogs
                    .Include(e => e.Blog_Users)
                        .ThenInclude(e => e.Articles)
                            .ThenInclude(e => e.Comments)
                    .Where(e => e.Site_Link == blog_link)
                    .SingleOrDefaultAsync();
            
            return web.Blog_Users;
        }
        
        public JwtSecurityToken TokenConstructor(List<Claim> claimList, string JwtKey, DateTime expiration )
        {
            JwtSecurityToken securityToken = null;
            if (claimList != null && string.IsNullOrWhiteSpace(JwtKey) && expiration > DateTime.Today)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                securityToken = 
                    new JwtSecurityToken(issuer:null, audience: null, claims:claimList, expires: expiration, signingCredentials:creds);

            }
            return securityToken;
        }
    }
}