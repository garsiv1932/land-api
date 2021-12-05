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

        public async Task<Web> getWebByLink(string webLink)
        {
            Web result = null;
            if (!string.IsNullOrWhiteSpace(webLink))
            {
                result = await _context.Db_Webs.FirstOrDefaultAsync(e => e.Site_Link == webLink);                
            }

            return result;
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
        public async Task<AuthAnswer>  AddJwtToSite(string website)
        {
            AuthAnswer response = null;
            if (!string.IsNullOrWhiteSpace(website))
            {
                var claimList = new List<Claim>()
                {
                    new Claim("WebLink", website)
                };
                
                DateTime expirationDate = DateTime.Now.AddYears(3);
                string securityToken = TokenConstructor(claimList, expirationDate);
                
               Web web = await _context.Db_Webs.Where(e => e.Site_Link == website).FirstOrDefaultAsync();

               if (web != null)
               {
                   web.Secret = securityToken;                   
                   await _context.SaveChangesAsync();
                   
                   response = new AuthAnswer()
                   {
                       Token = securityToken,
                       Expiration = expirationDate
                   };
               }
            }
            return response;
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

        public async Task<DtoWeb> getDtoWebByLink(string webLink)
        {
            DtoWeb result = null;
            if (string.IsNullOrWhiteSpace(webLink))
            {
                Web searchedWeb = await getWebByLink(webLink);
                if (searchedWeb != null)
                {
                    result = Utls.mapper.Map<DtoWeb>(searchedWeb);        
                }
            }
            return result;
        }
    }
}