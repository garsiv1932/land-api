using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class Blog_Article
    {
        public DateTime Blog_Article_Id { get; set; }
        public string Tittle;
        public string Link { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get;set; }

        public DateTime Publish_Date { get; set; }
        
        public List<Blog_Visit> Visits { get; set; }

        public int Blog_User_ID { get; set; }
        public Blog_User User { get; set; }
        
        public List<Blog_Article_Comment> Comments { get; set; }
        
        public Blog_Article( DateTime pID, string pLink, string pImage, Blog_User pUser)
        {
            Blog_Article_Id = pID;
            Image = pImage;
            Link = pLink;
            Likes = 0;
            Dislikes = 0;
            User = pUser;
            Comments = new List<Blog_Article_Comment>();
            Visits = new List<Blog_Visit>();
        }

        public Blog_Article()
        {
            Comments = new List<Blog_Article_Comment>();
            Visits = new List<Blog_Visit>();
        }
    }
}