using System;
using System.IdentityModel.Tokens.Jwt;

namespace Api.DTOs
{
    public class DtoWebAuthAnswer
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}