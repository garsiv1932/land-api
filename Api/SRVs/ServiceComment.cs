using Api.Context;
using Microsoft.Extensions.Configuration;

namespace Api.SRVs
{
    public class ServiceComment:Service
    {
        public ServiceComment(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public ServiceComment()
        {
            
        }
    }
}