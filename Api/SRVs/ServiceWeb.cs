using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Context;
using Api.DTOs;
using Api.Model;
using Api.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Api.SRVs
{
    public class ServiceWeb:Service
    {
        public ServiceWeb(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public ServiceWeb()
        {
            
        }

        public async Task<DtoWeb> getBlogsByLink(string webLink)
        {
            Web webSearched = await _context.Db_Webs.FirstOrDefaultAsync(e => e.Site_Link == webLink);
            return Utls.mapper.Map<DtoWeb>(webSearched);
        }
        
        public async Task<DtoWeb> getBlogs()
        {
            Web webSearched = await _context.Db_Webs.FirstOrDefaultAsync();
            var blogs = Utls.mapper.Map<DtoWeb>(webSearched);
            return blogs;

        }
        
        /*
         Sites has their own JWT Key for administragion purposes 
         */
        public async Task<DtoWebAuthAnswer>  AddJwtToSite(string website)
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
                   return new DtoWebAuthAnswer()
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
        
        public string TokenCreator(string website)
        {
            var claimList = new List<Claim>()
            {
                new Claim("WebLink", website)
            };
            
            DateTime expiration = DateTime.UtcNow.AddYears(3);
            
            return TokenConstructor(claimList, expiration );
        }

        private async Task<Web> webExist(string webSiteLink)
        {
            Web website = await _context.Db_Webs.Where(e => e.Site_Link == webSiteLink).SingleOrDefaultAsync();
            
            return website;
        }


    }
}