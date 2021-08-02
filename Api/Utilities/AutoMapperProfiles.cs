using Api.DTOs;
using Api.Model;
using AutoMapper;

namespace Api.Utilities
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Web,DTO_Blog >();
            CreateMap<DTO_Blog, Web>();
            CreateMap<DTO_Blog_Article, Web_Resource_Blog_Article>();
            CreateMap<DTO_Blog_Article_Comment, Web_Resource_Blog_Article_Comment>();
            CreateMap<Web,DTO_Blog >();
            CreateMap<Web_Resource_Blog_Article,DTO_Blog_Article>();
        }
    }
}