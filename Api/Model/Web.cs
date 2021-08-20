using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Model
{
    public class Web
    {
        public string Site_Link { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }


        public List<WebUser> users { get; set; }
        

        public Web( string pName, string pSiteLink)
        {
            Site_Link = pSiteLink;
            Name = pName;
            users = new List<WebUser>();
        }

        public Web()
        {
            users = new List<WebUser>();
        }

        public Web(string link)
        {
            Site_Link = link;
            users = new List<WebUser>();
        }
    }
}