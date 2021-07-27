using System;

namespace Api.Model
{
    public class Blog_Article_Comment
    {
        public DateTime Blog_Article_Comment_Id { get; set; }
        public DateTime Article_Id { get; set; }
        public Blog_Article Article { get; set; }
        public string Comment { get; set; }
        public DateTime Published { get; set; }
        public string Ip_Address { get; set; }

        public Blog_Article_Comment(DateTime pID,string pComment, DateTime pPublished)
        {
            Blog_Article_Comment_Id = pID;
            Comment = pComment;
            Published = pPublished;
        }

        public Blog_Article_Comment()
        {
        }
    }
}