using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Context;
using Api.DTOs;
using Api.Model;
using Api.SRVs;
using Api.Utilities;
using Microsoft.EntityFrameworkCore;


namespace Services.SRVs
{
    public abstract class Service : IDisposable,IService

    {
        protected ApiContext _context { get; }

        public Service(ApiContext context)
        {
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
        
        public async Task<List<Blog_User>> getUsersByBlogLink(string blog_link)
        {
            Blog _blog = await _context.Db_Blogs
                    .Include(e => e.Blog_Users)
                    .ThenInclude(e => e.Articles)
                    .ThenInclude(e => e.Comments)
                    .Where(e => e.Site_Link == blog_link)
                    .SingleOrDefaultAsync();
            
            return _blog.Blog_Users;
        }
    }
}