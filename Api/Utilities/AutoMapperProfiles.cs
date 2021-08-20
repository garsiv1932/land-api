using Api.DTOs;
using Api.Model;
using AutoMapper;

namespace Api.Utilities
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Web,DtoWeb >();
            CreateMap<DtoWeb, Web>();
            CreateMap<DtoWebArticle, WebArticle>();
            CreateMap<DtoWebArticleComment, WebArticleComment>();
            CreateMap<Web,DtoWeb >();
            CreateMap<WebArticle,DtoWebArticle>();
        }
    }
}