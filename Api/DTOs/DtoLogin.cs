using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class DtoLogin
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}