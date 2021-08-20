using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class WebUserRole
    {
        public string Name { get; set; }
        public List<WebUser> Users { get; set; }

        public WebUserRole(string name)
        {
            Name = name;
            Users = new List<WebUser>();
        }

        public WebUserRole()
        {
        }
    }
}