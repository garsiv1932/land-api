using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class Web_Article
    {
        public string Article_Link { get; set; }
        public DateTime Publish_Date { get; set; }
        public string Tittle { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        public int Visit_Count { get;set; }


        
        public List<Web_Visit> Visits { get; set; }

        public string Blog_User_Email { get; set; }
        public Web_User User { get; set; }
        
        public List<Web_Article_Comment> Comments { get; set; }
        
        public Web_Article( string pArticleLink, string pImage, Web_User pUser)
        {
            Image = pImage;
            Article_Link = pArticleLink;
            Likes = 0;
            Visit_Count = 0;
            User = pUser;
            Comments = new List<Web_Article_Comment>();
            Visits = new List<Web_Visit>();
        }

        public Web_Article()
        {
            Comments = new List<Web_Article_Comment>();
            Visits = new List<Web_Visit>();
        }
        
        public Web_Article( string pArticleLink, string pImage, Web_User pUser, string pTittle)
        {
            Image = pImage;
            Article_Link = pArticleLink;
            Likes = 0;
            Visit_Count = 0;
            User = pUser;
            Tittle = pTittle;
            Comments = new List<Web_Article_Comment>();
            Visits = new List<Web_Visit>();
        }
    }
}