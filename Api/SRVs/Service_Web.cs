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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.SRVs;

namespace Api.SRVs
{
    public class Service_Web:Service
    {
        public Service_Web(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public Service_Web()
        {
            
        }

        public async Task<DTO_Web> getBlogsByLink(string webLink)
        {
            Web webSearched = await _context.Db_Webs.FirstOrDefaultAsync(e => e.Site_Link == webLink);
            return Utls.mapper.Map<DTO_Web>(webSearched);
        }
        
        public async Task<DTO_Web> getBlogs()
        {
            Web webSearched = await _context.Db_Webs.FirstOrDefaultAsync();
            var blogs = Utls.mapper.Map<DTO_Web>(webSearched);
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
        public async Task<DTO_Web_AuthAnswer>  AddJwtToSite(string website)
        {
            if (!string.IsNullOrWhiteSpace(website))
            {
                var claimList = new List<Claim>()
                {
                    new Claim("WebLink", website)
                };
                
                DateTime expirationDate = DateTime.Now.AddYears(3);
                string securityToken = TokenConstructor(claimList, expirationDate);
                
               Web web = await _context.Db_Webs.Where(e => e.Site_Link == website).SingleAsync();
               
               web.Secret = securityToken.ToString();
               
               try
               {
                   await _context.SaveChangesAsync();
                   return new DTO_Web_AuthAnswer()
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
        
        private string TokenConstructor(string website )
        {
            var claimList = new List<Claim>()
            {
                new Claim("WebLink", website)
            };
            
            DateTime expiration = DateTime.UtcNow.AddYears(3);
            
            return TokenConstructor(claimList, expiration );
        }
        // Console.WriteLine(GetWeatherDisplay(15));  // output: Cold.
        // Console.WriteLine(GetWeatherDisplay(27));  // output: Perfect!
    }
}