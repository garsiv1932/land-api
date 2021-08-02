using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Context;
using Api.DTOs;
using Api.Model;
using Api.Utilities;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Services.SRVs
{
    public class Service_Blog:Service
    {
        public Service_Blog(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public async Task<DTO_Blog> getBlogsByLink(string webLink)
        {
            Web webSearched = await _context.Db_Blogs.FirstOrDefaultAsync(e => e.Site_Link == webLink);
            return Utls.mapper.Map<DTO_Blog>(webSearched);
        }
        
        public async Task<DTO_Blog> getBlogs()
        {
            Web webSearched = await _context.Db_Blogs.FirstOrDefaultAsync();
            var blogs = Utls.mapper.Map<DTO_Blog>(webSearched);
            return blogs;

        }
        
        public string GetWeatherDisplay(double tempInCelsius) => tempInCelsius < 20.0 ? "Cold." : "" ;

        public Web GetWeatherDisplay2(double tempInCelsius)
        {
            if (tempInCelsius < 20.0)
            {
                return new Web();
            }

            return new Web();
        }

        /*
         Sites has their own JWT Key for administragion purposes 
         */
        public async Task<DTO_Blog_AuthAnswer>  AddJwtToSite(string website, string JwtKey)
        {
            if (!string.IsNullOrWhiteSpace(website) && !string.IsNullOrWhiteSpace(JwtKey))
            {
                var claimList = new List<Claim>()
                {
                    new Claim("WebLink", website)
                };
                
                DateTime expirationDate = DateTime.Now.AddYears(3);
                JwtSecurityToken securityToken = TokenConstructor(claimList, JwtKey, expirationDate);
                
               Web web = await _context.Db_Blogs.Where(e => e.Site_Link == website).SingleAsync();
               
               web.Secret = securityToken;
               
               try
               {
                   await _context.SaveChangesAsync();
                   return new DTO_Blog_AuthAnswer()
                   {
                       Token = securityToken,
                       Expiration = expirationDate
                   };
               }
               catch (Exception e)
               {
                   Console.WriteLine(e);
                   throw new Exception(e.Message);
               }

            }

            return null;
        }
        
        private JwtSecurityToken TokenConstructor(string website, string JwtKey )
        {
            var claimList = new List<Claim>()
            {
                new Claim("WebLink", website)
            };
            
            DateTime expiration = DateTime.UtcNow.AddYears(3);
            
            return TokenConstructor(claimList, JwtKey, expiration );
        }
        // Console.WriteLine(GetWeatherDisplay(15));  // output: Cold.
        // Console.WriteLine(GetWeatherDisplay(27));  // output: Perfect!
    }
}