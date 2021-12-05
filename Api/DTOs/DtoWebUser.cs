using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class DtoWebUser
    {

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string WebLink { get; set; }
        public string UserName { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        
        public DateTime Born_Date { get; set; }
        public string Phone_Number { get; set; }
        public int Blog_User_Role_Id { get; set; }


        
    }
}