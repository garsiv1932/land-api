using System.Collections.Generic;

namespace Api.DTOs
{
    public class DTO_Blog
    {
        public string Name { get; set; }
        public string Site_Link { get; set; }
        public string Secret { get; set; }
        public List<DTO_Blog_User> Blog_Users { get; set; }
    }
}