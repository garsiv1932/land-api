using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class DTO_Blog_User
    {

        [Required]


        public string Password { get; set; }
        
        public int Blog_User_ID { get; set; }
        public string First_Name { get; set; }
        public string Second_Name { get; set; }
        public string First_Last_Name { get; set; }
        public string Second_Last_Name { get; set; }
        
        public string UserName { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        
        public DateTime Born_Date { get; set; }
        public string Password_Hash { get; set; }
        public string Phone_Number { get; set; }
        public int Access_Failed_Count { get; set; }

        public int Blog_Id { get; set; }

        
        public int Blog_User_Role_Id { get; set; }


        
    }
}