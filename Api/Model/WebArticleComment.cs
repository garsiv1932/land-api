using System;

namespace Api.Model
{
    public class WebArticleComment
    {
        public string UserEmail { get; set; }
        public DateTime Published { get; set; }
        
        public WebUser User { get; set; }
        public WebArticle Article { get; set; }
        
        public string Comment { get; set; }
        
        public string IpAddress { get; set; }

        public WebArticleComment(DateTime pID,string pComment, DateTime pPublished)
        {
            Comment = pComment;
            Published = pPublished;
        }

        public WebArticleComment()
        {
        }
    }
}