using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class WebArticle
    {
        public string ArticleLink { get; set; }
        public DateTime PublishDate { get; set; }
        public string Tittle { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        public int VisitCount { get; set; }


        
        public List<WebVisit> Visits { get; set; }

        public string UserEmail { get; set; }
        public WebUser User { get; set; }
        
        public List<WebArticleComment> Comments { get; set; }
        
        public WebArticle( string pArticleLink, string pImage, WebUser pUser)
        {
            Image = pImage;
            ArticleLink = pArticleLink;
            Likes = 0;
            VisitCount = 0;
            User = pUser;
            Comments = new List<WebArticleComment>();
            Visits = new List<WebVisit>();
        }

        public WebArticle()
        {
            Comments = new List<WebArticleComment>();
            Visits = new List<WebVisit>();
        }
        
        public WebArticle( string pArticleLink, string pImage, WebUser pUser, string pTittle)
        {
            Image = pImage;
            ArticleLink = pArticleLink;
            Likes = 0;
            VisitCount = 0;
            User = pUser;
            Tittle = pTittle;
            Comments = new List<WebArticleComment>();
            Visits = new List<WebVisit>();
        }
    }
}