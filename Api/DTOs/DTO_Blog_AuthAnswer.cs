using System;
using System.IdentityModel.Tokens.Jwt;

namespace Api.DTOs
{
    public class DTO_Blog_AuthAnswer
    {
        public JwtSecurityToken Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}