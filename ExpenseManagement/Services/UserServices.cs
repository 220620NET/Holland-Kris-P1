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
        public Users GetUserByuserId(int userId)
        {
            try
            {
                return new UserRepository().GetUserById(userId);
            }
            catch (ResourceNotFoundException)
            {
                throw;
            }
        }
    }
}
