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
            cfg.CreateMap<DTO_Web_Article, Web_Article>();
            cfg.CreateMap<Web_Article,DTO_Web_Article>();
            
            cfg.CreateMap<DTO_Web, Web>();
            cfg.CreateMap<Web,DTO_Web >();
            
            cfg.CreateMap<DTO_Web_Article_Comment, Web_Article_Comment>();
            cfg.CreateMap<Web_Article_Comment,DTO_Web_Article_Comment>();
            
            cfg.CreateMap<Web_User, DTO_Web_User>();
            cfg.CreateMap<DTO_Web_User,Web_User>();

            cfg.CreateMap<DTO_Web_Visit, Web_Visit>();
            cfg.CreateMap<Web_Visit,DTO_Web_Visit>();
        });

        public static Mapper mapper { get; set; } = new(config);
        
        
        
        
    }
}