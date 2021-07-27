using Api.DTOs;
using Api.Model;
using AutoMapper;

namespace Api.Utilities
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Blog,DTO_Blog >();
            CreateMap<DTO_Blog, Blog>();
            CreateMap<DTO_Blog_Article, Blog_Article>();
            CreateMap<DTO_Blog_Article_Comment, Blog_Article_Comment>();
            CreateMap<Blog,DTO_Blog >();
            CreateMap<Blog_Article,DTO_Blog_Article>();
        }
    }
}