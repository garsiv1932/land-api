using System.Collections.Generic;

namespace Api.DTOs
{
    public class DtoWeb
    {
        public string Name { get; set; }
        public string Site_Link { get; set; }
        public string Secret { get; set; }
        public List<DtoWebUser> Blog_Users { get; set; }
    }
}