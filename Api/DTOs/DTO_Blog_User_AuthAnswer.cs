using System;

namespace Api.DTOs
{
    public class DTO_Blog_User_AuthAnswer
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}