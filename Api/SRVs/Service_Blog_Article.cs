using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Utilities;
using Api.Context;
using Api.Model;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Services.SRVs;

namespace Api.SRVs
{
    public class Service_Blog_Article:Service
    {
        public async Task<List<DTO_Blog_Article>> get_DTO_Articles_ByLink(string link)
        {
            List<Web_Resource_Blog_Article> articles = new List<Web_Resource_Blog_Article>();
            List<Web_User> users = await getUsersByBlogLink(link);

            foreach (var usr in users)
            {
                articles.AddRange(usr.Articles);
            }
            
            return Utls.mapper.Map<List<Web_Resource_Blog_Article>, List<DTO_Blog_Article> >(articles);
        }

        private IEnumerable<Web_Resource_Blog_Article> get_Artycles_FromUser(string usrEmail)
        {
            return _context.Db_Articles.Where(e => e.User.Email == usrEmail);
        }

        public async Task<List<DTO_Blog_Article>> get_DTO_Artycles_FromUser(string email)
        {
            Web_User user = await _context.Db_Blog_User.Where(e => e.Email == email).SingleOrDefaultAsync();
            return Utls.mapper.Map<List<DTO_Blog_Article>>(user.Articles) ;
        }

        public Service_Blog_Article():base()
        {
            
        }

        public Service_Blog_Article(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {
            
        }


        public async Task<List<DTO_Blog_Article_Comment>> getCommentsByArticleID(string articleLink)
        {
            Web_Resource_Blog_Article resourceBlogArticle =await _context.Db_Articles.Where(e => e.Article_Link == articleLink).SingleAsync();
            return Utls.mapper.Map<List<DTO_Blog_Article_Comment>>(resourceBlogArticle.Comments);
        }

        public async Task<bool> articleExist(string articleLink)
        {
            Web_Resource_Blog_Article resourceBlogArticle = await _context.Db_Articles.Where(e => e.Article_Link == articleLink)
                .SingleOrDefaultAsync();

            if (resourceBlogArticle != null)
            {
                return true;
            }

            return false;

        }
    }
}