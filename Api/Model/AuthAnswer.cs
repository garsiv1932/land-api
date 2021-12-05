using System;

namespace Api.Model
{
    public class AuthAnswer
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}