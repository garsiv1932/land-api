using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class Blog_User_Role
    {
        public DateTime Blog_User_Role_Id { get; set; }
        public string Name { get; set; }
        public List<Blog_User> Users { get; set; }

        public Blog_User_Role(DateTime pID,string name)
        {
            Blog_User_Role_Id = pID;
            Name = name;
            Users = new List<Blog_User>();
        }

        public Blog_User_Role()
        {
        }
    }
}