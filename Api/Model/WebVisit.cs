using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class WebVisit
    {
        public string IpAddress { get; set; }
        public DateTime VisitDate { get; set; }

        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string VisitedResource { get; set; }

        public WebVisit(int blogVisitId, string ipAddress, DateTime visitDate)
        {
            IpAddress = ipAddress;
            VisitDate = visitDate;
        }

        public WebVisit()
        {
        }
    }
}