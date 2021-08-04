using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class DTO_Login
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}