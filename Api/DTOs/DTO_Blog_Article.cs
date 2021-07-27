using System;
using System.Collections.Generic;


namespace Api.DTOs
{
    public class DTO_Blog_Article
    {
        // public DTO_Blog_User User { get; set; }
        //public int Blog_User_ID { get; set; }
        public string Tittle;
        public string Link { get; set; }
        public DateTime Publish_Date { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get;set; }
        // public List<DTO_Blog_Visit> Visits { get; set; }
        public List<DTO_Blog_Article_Comment> Comments { get; set; }


        public DTO_Blog_Article()
        {
            Comments = new List<DTO_Blog_Article_Comment>();
        }
    }
    
    
}