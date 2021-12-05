using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class DtoWeb
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Site_Link { get; set; }
        public string Secret { get; set; }
        public List<DtoWebUser> Blog_Users { get; set; }
    }
}