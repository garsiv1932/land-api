using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class Blog_User
    {
        public DateTime Blog_User_ID { get; set; }
        public string First_Name { get; set; }
        public string Second_Name { get; set; }
        public string First_Last_Name { get; set; }
        public string Second_Last_Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        
        public DateTime Born_Date { get; set; }
        public string Password_Hash { get; set; }
        public string Phone_Number { get; set; }
        public int Access_Failed_Count { get; set; }

        public int Blog_Id { get; set; }
        public Blog Blog { get; set; }
        
        public int Blog_User_Role_Id { get; set; }
        public Blog_User_Role Role { get; set; }
        public List<Blog_Article> Articles { get; set; }

        
        public List<Blog_Article> getArticles()
        {
            return this.Articles;
        }

        public Blog_User( DateTime pID,string firstName, string secondName, string firstLastName, string secondLastName, string userName, string email, DateTime bornDate, string passwordHash, string phoneNumber, Blog blog, Blog_User_Role role)
        {
            Blog_User_ID = pID;
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
            Blog = blog;
            Role = role;
            Articles = new List<Blog_Article>();
        }

        public Blog_User()
        {
        }
    }
}