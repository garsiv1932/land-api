using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Model
{
    public class Web
    {
        public string Site_Link { get; set; }
        public string Name { get; set; }
        public JwtSecurityToken Secret { get; set; }

        public List<Web_User> Blog_Users { get; set; }

        public Web( string pName, string pSiteLink)
        {
            Site_Link = pSiteLink;
            Name = pName;
            Blog_Users = new List<Web_User>();
        }

        public Web()
        {
            Blog_Users = new List<Web_User>();
        }
    }
}