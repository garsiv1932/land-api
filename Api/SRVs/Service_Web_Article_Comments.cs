using Api.Context;
using Api.SRVs;
using Microsoft.Extensions.Configuration;

namespace Services.SRVs
{
    public class Service_Web_Article_Comments:Service
    {
        public Service_Web_Article_Comments(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public Service_Web_Article_Comments()
        {
            
        }
    }
}