using Api.Context;
using AutoMapper.Configuration;

namespace Services.SRVs
{
    public class Service_Blog_Article_Comments:Service
    {
        public Service_Blog_Article_Comments(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}