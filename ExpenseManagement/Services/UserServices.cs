using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccess;
using CustomExceptions;

namespace Services
{
    public class UserServices
    {
        //GetUserByUsername
        public Users GetUserByUsername(string username)
        {
            try
            {
                return new UserRepository().GetUserByUsername(username);
            }
            catch (UsernameNotAvailable)
            {
                throw;
            }
        }
        //GetAllUsers
        public List<Users> GetAllUsers()
        {
            try
            {
                return new UserRepository().GetAllUsers();
            }
            catch (ResourceNotFoundException)
            {
                throw;
            }
        }
        //GetUserByuserID
        public Users GetUserByuserId(int userId)
        {
            Users users = new Users();
            try
            {
                List<Users> userList = GetAllUsers();
                if (userList.Count <userId)
                {
                    throw new ResourceNotFoundException();
                }
                else
                {
                    users =new UserRepository().GetUserById(userId);
                }
            }
            catch (ResourceNotFoundException)
            {
                throw;
            }
            return users;
        }
    }
}
