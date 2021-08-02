using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class Web_User_Role
    {
        public string Name { get; set; }
        public List<Web_User> Users { get; set; }

        public Web_User_Role(DateTime pID,string name)
        {
            Name = name;
            Users = new List<Web_User>();
        }

        public Web_User_Role()
        {
        }
    }
}