using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Utilities;
using Api.Context;
using Api.Model;
using Microsoft.EntityFrameworkCore;
using Services.SRVs;

namespace Api.SRVs
{
    public class Service_Blog_Article:Service
    {
        public async Task<List<DTO_Blog_Article>> get_DTO_Articles_ByLink(string link)
        {
            List<Blog_Article> articles = new List<Blog_Article>();
            List<Blog_User> users = await getUsersByBlogLink(link);

            foreach (var usr in users)
            {
                articles.AddRange(usr.Articles);
            }
            
            return Utls.mapper.Map<List<Blog_Article>, List<DTO_Blog_Article> >(articles);
        }

        private IEnumerable<Blog_Article> get_Artycles_FromUser(string usrEmail)
        {
            return _context.Db_Articles.Where(e => e.User.Email == usrEmail);
        }

        public async Task<List<DTO_Blog_Article>> get_DTO_Artycles_FromUser(string email)
        {
            Blog_User user = await _context.Db_Blog_User.Where(e => e.Email == email).SingleOrDefaultAsync();
            return Utls.mapper.Map<List<DTO_Blog_Article>>(user.Articles) ;
        }

        public Service_Blog_Article():base()
        {
            
        }

        public Service_Blog_Article(ApiContext context) : base(context)
        {
            
        }


        public async Task<List<DTO_Blog_Article_Comment>> getCommentsByArticleID(DateTime articleId)
        {
            Blog_Article article =await _context.Db_Articles.Where(e => e.Blog_Article_Id == articleId).SingleAsync();
            return Utls.mapper.Map<List<DTO_Blog_Article_Comment>>(article.Comments);
        }

        public async Task<bool> articleExist(DateTime articleId)
        {
            Blog_Article article = await _context.Db_Articles.Where(e => e.Blog_Article_Id == articleId)
                .SingleOrDefaultAsync();

            if (article != null)
            {
                return true;
            }

            return false;

        }
    }
}