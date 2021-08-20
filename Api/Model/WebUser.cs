using System;
using System.Collections.Generic;

namespace Api.Model
{
    public class WebUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string UserName { get; set; }
        public DateTime BornDate { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public int AccessFailedCount { get; set; }

        public string SiteLink { get; set; }
        public Web Web { get; set; }

        public string CvLink { get; set; }
        
        public int RoleId { get; set; }
        public WebUserRole Role { get; set; }
        public List<WebArticle> Articles { get; set; }
        
        public List<WebArticleComment> Comments { get; set; }

        public List<Logs> Logs { get; set; }


        public List<WebArticle> getArticles()
        {
            return this.Articles;
        }

        public WebUser( string firstName, string secondName, string firstLastName, string secondLastName, string userName, string email, DateTime bornDate, string passwordHash, string phoneNumber, Web web, WebUserRole role)
        {
            FirstName = firstName;
            SecondName = secondName;
            FirstLastName = firstLastName;
            SecondLastName = secondLastName;
            UserName = userName;
            Email = email;
            BornDate = bornDate;
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordHash);
            PhoneNumber = phoneNumber;
            AccessFailedCount = 0;
            Web = web;
            Role = role;
            Articles = new List<WebArticle>();
        }

        public WebUser()
        {
        }
    }
}