using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Utilities;
using Api.Context;
using Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.SRVs
{
    public class ServiceArticle:Service
    {
        public ServiceArticle(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {
            
        }
        
        public async Task<List<DtoWebArticle>> getDtoArticlesByLink(string link)
        {
            List<DtoWebArticle> dtoArts = null;
 
            if (!string.IsNullOrWhiteSpace(link))
            {
                List<WebUser> users = await getUsersByBlogLink(link);

                if (users != null)
                {
                    List<WebArticle> arts = new List<WebArticle>();
                    foreach (var usr in users)
                    {
                        arts.AddRange(usr.Articles);
                    }
                    dtoArts = new List<DtoWebArticle>();
                    dtoArts= Utls.mapper.Map<List<WebArticle>, List<DtoWebArticle>>(arts);
                }
            }
            return dtoArts;
        }

        


        private IEnumerable<WebArticle> getArtyclesFromUser(string userEmail)
        {
            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                return _context.DbArticles.Where(e => e.User.Email == userEmail);
            }
            throw new Exception(Errors.UnknownError);
        }

        public async Task<List<DtoWebArticle>> getDtoArtyclesFromUser(string email)
        {
            WebUser user = await _context.DbWebUser
                .Include(e => e.Articles)
                .Where(e => e.Email == email)
                .SingleOrDefaultAsync();
            return Utls.mapper.Map<List<DtoWebArticle>>(user.Articles) ;
        }

        public ServiceArticle()
        {
            
        }
    
        public async Task<List<DtoWebArticleComment>> getCommentsByArticleID(string articleLink)
        {
            WebArticle article =await _context.DbArticles.Where(e => e.ArticleLink == articleLink).SingleAsync();
            return Utls.mapper.Map<List<DtoWebArticleComment>>(article.Comments);
        }

        public async Task<bool> articleExist(string articleLink)
        {
            WebArticle article = await _context.DbArticles.Where(e => e.ArticleLink == articleLink)
                .SingleOrDefaultAsync();

            if (article != null)
            {
                return true;
            }

            return false;

        }

        public async Task<int> cantidadArticulos(string link)
        {
            if (!string.IsNullOrWhiteSpace(link))
            {
                int cantidad = 0;
                List<WebUser> users = await usersByWebLink(link);
                if (users == null)
                {
                    throw new Exception(Errors.ElementNotFound);
                }
                else
                {
                    foreach (WebUser user in users)
                    {
                        cantidad += user.Articles.Count;
                    }

                    return cantidad;
                }
            }

            throw new Exception(Errors.UnknownError);
        }


    }
}