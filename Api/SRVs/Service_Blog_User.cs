using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Model;
using AutoMapper;
using Api.Context;
using Api.Utilities;
using Microsoft.EntityFrameworkCore;
using Services.SRVs;

namespace Api.SRVs
{
    public class Service_Blog_User:Service
    {
        private readonly IMapper _mapper;

        public Service_Blog_User(ApiContext context, IMapper mapper):base(context)
        {
            _mapper = mapper;
        }

        public async Task<Blog_User> createUser(DTO_Blog_User userDTO)
        {
            Blog_User user = _mapper.Map<Blog_User>(userDTO);
            var user_created = await _context.Db_Blog_User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;        
        }


        public async Task<bool> userExist(DTO_Blog_User user)
        {
            bool exist = false;
            if (user != null)
            {
                Blog_User user_exist = await _context.Db_Blog_User.AsQueryable().Where(e => e.Email== user.Email).SingleAsync();
                if (user_exist != null)
                {
                    exist = true;
                    return exist;
                }
            }
            return exist;
        }

        public async Task<List<DTO_Blog_User>> get_DTO_Users_By_BlogLink(string blog_link)
        {
            List<Blog_User> users = await getUsersByBlogLink(blog_link);
            
            return Utls.mapper.Map<List<DTO_Blog_User>>(users);
        }
        
        public async Task<Blog_User> userLogin(DTO_Blog_User user)
        {
            Blog_User user_login = null;
            if (user != null)
            {
                user_login = await _context.Db_Blog_User.AsQueryable().Where(e => e.Email == user.Email).SingleAsync();
            }
            return user_login;
        }

        public bool passwordValid(string password)
        {
            bool at_least_one_upper = false;
            bool oneNumber = false;
            bool at_least_eight_chars = false;

            if (password.Length > 8)
            {
                at_least_eight_chars = true;
                foreach (char chr in password)
                {
                    string upper = chr.ToString().ToUpper();
                    
                    if (upper == chr.ToString())
                    {
                        at_least_one_upper = true;
                    }

                    int number_check;
                    oneNumber = int.TryParse(chr.ToString(), out number_check);
                }
            }

            return oneNumber && at_least_one_upper && at_least_eight_chars;
        }
    }
}