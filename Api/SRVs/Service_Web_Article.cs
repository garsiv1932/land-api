using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Utilities;
using Api.Context;
using Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.SRVs;

namespace Api.SRVs
{
    public class Service_Web_Article:Service
    {


        public async Task<List<DTO_Web_Article>> get_DTO_Articles_ByLink(string link)
        {
            List<Web_Article> articles = new List<Web_Article>();
            List<Web_User> users = await getUsersByBlogLink(link);

            foreach (var usr in users)
            {
                articles.AddRange(usr.Articles);
            }
            
            return Utls.mapper.Map<List<Web_Article>, List<DTO_Web_Article>>(articles);
        }

        private IEnumerable<Web_Article> get_Artycles_FromUser(string usrEmail)
        {
            return _context.Db_Articles.Where(e => e.User.Email == usrEmail);
        }

        public async Task<List<DTO_Web_Article>> get_DTO_Artycles_FromUser(string email)
        {
            Web_User user = await _context.Db_Web_User.Where(e => e.Email == email).SingleOrDefaultAsync();
            return Utls.mapper.Map<List<DTO_Web_Article>>(user.Articles) ;
        }

        public Service_Web_Article()
        {
            
        }

        public Service_Web_Article(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {
            
        }

    
        public async Task<List<DTO_Web_Article_Comment>> getCommentsByArticleID(string articleLink)
        {
            Web_Article article =await _context.Db_Articles.Where(e => e.Article_Link == articleLink).SingleAsync();
            return Utls.mapper.Map<List<DTO_Web_Article_Comment>>(article.Comments);
        }

        public async Task<bool> articleExist(string articleLink)
        {
            Web_Article article = await _context.Db_Articles.Where(e => e.Article_Link == articleLink)
                .SingleOrDefaultAsync();

            if (article != null)
            {
                return true;
            }

            return false;

        }
    }
}