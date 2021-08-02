using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.DTOs;
using Api.Model;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


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
            cfg.CreateMap<DTO_Blog_Article, Web_Resource_Blog_Article>();
            cfg.CreateMap<Web_Resource_Blog_Article,DTO_Blog_Article>();
            
            cfg.CreateMap<DTO_Blog, Web>();
            cfg.CreateMap<Web,DTO_Blog >();
            
            cfg.CreateMap<DTO_Blog_Article_Comment, Web_Resource_Blog_Article_Comment>();
            cfg.CreateMap<Web_Resource_Blog_Article_Comment,DTO_Blog_Article_Comment>();
            
            cfg.CreateMap<Web_User, DTO_Blog_User>();
            cfg.CreateMap<DTO_Blog_User,Web_User>();
        });

        public static Mapper mapper { get; set; } = new(config);
        
        
        
        
    }
}