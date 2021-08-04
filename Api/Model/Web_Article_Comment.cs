using System;

namespace Api.Model
{
    public class Web_Article_Comment
    {
        public string User_Email { get; set; }
        public DateTime Published { get; set; }
        
        public Web_User User { get; set; }
        public Web_Article Article { get; set; }
        
        public string Comment { get; set; }
        
        public string Ip_Address { get; set; }

        public Web_Article_Comment(DateTime pID,string pComment, DateTime pPublished)
        {
            Comment = pComment;
            Published = pPublished;
        }

        public Web_Article_Comment()
        {
        }
    }
}