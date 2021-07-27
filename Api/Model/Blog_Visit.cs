using System;

namespace Api.Model
{
    public class Blog_Visit
    {
        public DateTime Blog_Visit_ID { get; set; }
        public string Ip_Addr { get; set; }
        public DateTime Visit_Date { get; set; }

        public Blog_Visit(DateTime blogVisitId, string ipAddr, DateTime visitDate)
        {
            Blog_Visit_ID = blogVisitId;
            Ip_Addr = ipAddr;
            Visit_Date = visitDate;
        }

        public Blog_Visit()
        {
        }
    }
}