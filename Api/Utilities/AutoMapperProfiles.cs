using Api.DTOs;
using Api.Model;
using AutoMapper;

namespace Api.Utilities
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Web,DTO_Web >();
            CreateMap<DTO_Web, Web>();
            CreateMap<DTO_Web_Article, Web_Article>();
            CreateMap<DTO_Web_Article_Comment, Web_Article_Comment>();
            CreateMap<Web,DTO_Web >();
            CreateMap<Web_Article,DTO_Web_Article>();
        }
    }
}