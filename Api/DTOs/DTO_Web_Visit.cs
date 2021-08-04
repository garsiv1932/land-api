using System;

namespace Api.DTOs
{
    public class DTO_Web_Visit
    {
        public string Ip_Addr { get; set; }
        public DateTime Visit_Date { get; set; }

        public string country { get; set; }
        public string country_code { get; set; }
        public string recurso_visitado { get; set; }
    }
}