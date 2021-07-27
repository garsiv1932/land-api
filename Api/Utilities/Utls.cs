using System.Collections.Generic;
using Api.DTOs;
using Api.Model;
using AutoMapper;



namespace Api.Utilities
{
    public class Utls
    {
        private static MapperConfiguration config = new MapperConfiguration(cfg => { 
            cfg.CreateMap<DTO_Blog_Article, Blog_Article>();
            cfg.CreateMap<Blog_Article,DTO_Blog_Article>();
            
            cfg.CreateMap<DTO_Blog, Blog>();
            cfg.CreateMap<Blog,DTO_Blog >();
            
            cfg.CreateMap<DTO_Blog_Article_Comment, Blog_Article_Comment>();
            cfg.CreateMap<Blog_Article_Comment,DTO_Blog_Article_Comment>();
            
            cfg.CreateMap<Blog_Article_Comment, DTO_Blog_Article_Comment>();
            cfg.CreateMap<DTO_Blog_Article_Comment,Blog_Article_Comment>();
            
            

        });

        public static Mapper mapper { get; set; } = new(config);
    }
}