using System;
using System.IdentityModel.Tokens.Jwt;

namespace Api.DTOs
{
    public class DTO_Web_AuthAnswer
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}