using System;

namespace Api.DTOs
{
    public class DTO_Web_Article_Comment
    {
        //public int Article_Id { get; set; }
        //public DTO_Blog_Article Article { get; set; }
        public string Comment { get; set; }
        public DateTime Published { get; set; }
        public string Ip_Address { get; set; }
    }
}