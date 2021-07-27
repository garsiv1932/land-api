using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Context;
using Api.DTOs;
using Api.Model;
using Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Services.SRVs
{
    public class Service_Blog:Service
    {
        public Service_Blog(ApiContext context) : base(context)
        {
        }

        public async Task<DTO_Blog> getBlogsByLink(string webLink)
        {
            Blog blogSearched = await _context.Db_Blogs.FirstOrDefaultAsync(e => e.Site_Link == webLink);
            return Utls.mapper.Map<DTO_Blog>(blogSearched);
        }
        
        public async Task<DTO_Blog> getBlogs()
        {
            Blog blogSearched = await _context.Db_Blogs.FirstOrDefaultAsync();
            var blogs = Utls.mapper.Map<DTO_Blog>(blogSearched);
            return blogs;

        }
        

    }
}