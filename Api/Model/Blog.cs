using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class Blog
    { 
        public DateTime Blog_Id { get; set; }
        public string Name { get; set; }
        public string Site_Link { get; set; }
        public string Secret { get; set; }

        public List<Blog_User> Blog_Users { get; set; }

        public Blog( DateTime pID, string pName, string pSiteLink)
        {
            Blog_Id = pID;
            Site_Link = pSiteLink;
            Name = pName;
            Blog_Users = new List<Blog_User>();
        }

        public Blog()
        {
            Blog_Users = new List<Blog_User>();
        }
    }
}