using Api.DTOs;
using Api.Model;
using AutoMapper;


namespace Api.Utilities
{
    public class Utls
    {
        
        public enum AuthEnthity
        {
            WebSite,
            User
        }

        private static MapperConfiguration config = new MapperConfiguration(cfg => { 
            cfg.CreateMap<DtoWebArticle, WebArticle>();
            cfg.CreateMap<WebArticle,DtoWebArticle>();
            
            cfg.CreateMap<DtoWeb, Web>();
            cfg.CreateMap<Web,DtoWeb >();
            
            cfg.CreateMap<DtoWebArticleComment, WebArticleComment>();
            cfg.CreateMap<WebArticleComment,DtoWebArticleComment>();
            
            cfg.CreateMap<WebUser, DtoWebUser>();
            cfg.CreateMap<DtoWebUser,WebUser>();

            cfg.CreateMap<DtoWebVisit, WebVisit>();
            cfg.CreateMap<WebVisit,DtoWebVisit>();
        });

        public static Mapper mapper { get; set; } = new(config);
        
        
        
        
    }
}