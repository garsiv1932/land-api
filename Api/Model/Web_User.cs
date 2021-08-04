using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class Web_User
    {
        public string Email { get; set; }
        public string First_Name { get; set; }
        public string Second_Name { get; set; }
        public string First_Last_Name { get; set; }
        public string Second_Last_Name { get; set; }
        public string UserName { get; set; }
        public DateTime Born_Date { get; set; }
        public string Password_Hash { get; set; }
        public string Phone_Number { get; set; }
        public int Access_Failed_Count { get; set; }

        public string Site_Link { get; set; }
        public Web Web { get; set; }

        public string cv_link { get; set; }
        
        public int Blog_User_Role_Id { get; set; }
        public Web_User_Role Role { get; set; }
        public List<Web_Article> Articles { get; set; }
        
        public List<Web_Article_Comment> Comments { get; set; }

        
        public List<Web_Article> getArticles()
        {
            return this.Articles;
        }

        public Web_User( string firstName, string secondName, string firstLastName, string secondLastName, string userName, string email, DateTime bornDate, string passwordHash, string phoneNumber, Web web, Web_User_Role role)
        {
            First_Name = firstName;
            Second_Name = secondName;
            First_Last_Name = firstLastName;
            Second_Last_Name = secondLastName;
            UserName = userName;
            Email = email;
            Born_Date = bornDate;
            Password_Hash = BCrypt.Net.BCrypt.HashPassword(passwordHash);
            Phone_Number = phoneNumber;
            Access_Failed_Count = 0;
            Web = web;
            Role = role;
            Articles = new List<Web_Article>();
        }

        public Web_User()
        {
        }
    }
}