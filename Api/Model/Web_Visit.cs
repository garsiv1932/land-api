using System;
using System.Collections.Generic;
using Services.SRVs;

namespace Api.Model
{
    public class Web_Visit
    {
        public string Ip_Addr { get; set; }
        public DateTime Visit_Date { get; set; }

        public string country { get; set; }
        public string country_code { get; set; }
        public Web_Resource recurso_visitado { get; set; }

        public Web_Visit(int blogVisitId, string ipAddr, DateTime visitDate)
        {
            Ip_Addr = ipAddr;
            Visit_Date = visitDate;
        }

        public Web_Visit()
        {
        }
    }
}