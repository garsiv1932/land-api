using System;
using System.Collections.Generic;


namespace Api.DTOs
{
    public class DTO_Web_Article
    {
        // public DTO_Blog_User User { get; set; }
        //public int Blog_User_ID { get; set; }
        public string Article_Link { get; set; }
        public DateTime Publish_Date { get; set; }
        public string Tittle { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        public int Visit_Count { get;set; }
        // public List<DTO_Blog_Visit> Visits { get; set; }
        public List<DTO_Web_Article_Comment> Comments { get; set; }
        public DTO_Web_User User { get; set; }


        public DTO_Web_Article()
        {
            Comments = new List<DTO_Web_Article_Comment>();
        }
    }
    
    
}