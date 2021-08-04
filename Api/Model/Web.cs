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


        public List<Web_User> users { get; set; }
        

        public Web( string pName, string pSiteLink)
        {
            Site_Link = pSiteLink;
            Name = pName;
            users = new List<Web_User>();
        }

        public Web()
        {
            users = new List<Web_User>();
        }

        public Web(string link)
        {
            Site_Link = link;
            users = new List<Web_User>();
        }
    }
}