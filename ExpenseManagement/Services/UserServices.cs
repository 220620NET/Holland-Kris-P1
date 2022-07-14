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
        private readonly IUserDAO _userDAO;
        public UserServices(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        /// <summary>
        /// Service that will retrieve a single user with a specified username, mainly used in username verification
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User with specified username</returns>
        /// <exception cref="UsernameNotAvailable">Occurs if no such username exists or if table is null</exception>
        public Users GetUserByUsername(string username)
        {
            try
            {
                return _userDAO.GetUserByUsername(username);
            }
            catch (UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();
            }
        }
        
        /// <summary>
        /// Service that will retrieve all users
        /// </summary>
        /// <returns>List of all users</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the table is null</exception>
        public List<Users> GetAllUsers()
        {
            try
            {
                return _userDAO.GetAllUsers();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        
        /// <summary>
        /// Service to retireve a specific user witha  provided id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Specific user with provided ID</returns>
        /// <exception cref="UsernameNotAvailable">NO user exists with that userID</exception>
        /// <exception cref="ResourceNotFoundException">Occurs when Table is null</exception>
        public Users GetUserByuserId(int userId)
        {
            Users users;
            try
            {
                List<Users> userList = GetAllUsers();
                if (userList.Count <userId)
                {
                    throw new UsernameNotAvailable();
                }
                else
                {
                    users =_userDAO.GetUserById(userId);
                }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            catch (UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();
            }
            return users;
        }
    }
}
