using System.Data.SqlClient;
using sensitive;
using Microsoft.EntityFrameworkCore;
using Models;
using CustomExceptions; 

namespace DataAccess
{
    public class UserRepository: IUserDAO
    {
        //Dependency injection
        private readonly ExpenseDbContext _expenseDbContext;
        public UserRepository(ExpenseDbContext expenseDbContext)
        {
            _expenseDbContext=expenseDbContext;
        }
        /// <summary>
        /// This will create an instance of the SQL command SELECT * FROM P1.users;
        /// </summary>
        /// <returns>List of all users in the database</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if either the database does not exist or if the table is empty</exception>
        public List<Users> GetAllUsers()
        {
            try
            { 
               return _expenseDbContext.users.ToList();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            } 
        }
       
        /// <summary>
        /// This method will create an instance of the SQL command SELECT*FROM P1.users WHERE userID = <input>"userID"</input>
        /// </summary>
        /// <param name="userId">A valid userID</param>
        /// <returns></returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the userId does not exist in the table</exception>
        public Users GetUserById(int? userId)
        {
            try
            {
                return _expenseDbContext.users.FirstOrDefault(p => p.userId == userId) ?? throw new ResourceNotFoundException();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            } 
        }
      
        /// <summary>
        /// This method will create an instance of the SQL command SELECT*FROM P1.users WHERE username=<input>param</input>
        /// </summary>
        /// <param name="username">A valid username</param>
        /// <returns>User with specified username</returns>
        /// <exception cref="UsernameNotAvailable">Occurs if the username is not valid</exception>
        /// <exception cref="ResourceNotFoundException">Occurs if the username is not in the database</exception>
        public Users GetUserByUsername(string? username)
        {
            try
            {
                return _expenseDbContext.users.FirstOrDefault(p => p.username == username) ?? throw new ResourceNotFoundException();
            }
            catch (UsernameNotAvailable )
            {
                throw new UsernameNotAvailable();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            } 
        }
       
        /// <summary>
        /// Method to create a new user replicating the insert into DML command
        /// </summary>
        /// <param name="newUser">A new user with no specified userID</param>
        /// <returns>The user after being created</returns>
        /// <exception cref="UsernameNotAvailable"></exception>
        public Users CreateUser(Users newUser)
        { 
            try
            { 
                if (newUser.username != null)
                {
                    _expenseDbContext.users.Add(newUser);
                    _expenseDbContext.SaveChanges();
                    _expenseDbContext.ChangeTracker.Clear();
                    return newUser;
                }
                else
                {
                    throw new UsernameNotAvailable();
                }
            }
            catch (UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();
            } 
        }
        /// <summary>
        /// This represents the update sql command for changing the password of a given user id
        /// </summary>
        /// <param name="user">The user to change passwords of</param>
        /// <exception cref="ResourceNotFoundException">That user doesn't exist</exception>
        public void ResetPassword(Users user)
        { 
            try
            {
                _expenseDbContext.users.Update(user);
                _expenseDbContext.SaveChanges();
                _expenseDbContext.ChangeTracker.Clear();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This represents the update sql command to change the role of a given user id
        /// </summary>
        /// <param name="user">The user to change and their role</param>
        /// <exception cref="ResourceNotFoundException">That user doesn't exist</exception>
        public void PayRollChange(Users user)
        { 
            try
            {
                _expenseDbContext.users.Update(user);
                _expenseDbContext.SaveChanges();
                _expenseDbContext.ChangeTracker.Clear();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        public void DeleteUser(int id)
        { 
            try
            {
                _expenseDbContext.Remove(new Users { userId=id});
                _expenseDbContext.SaveChanges();
                _expenseDbContext.ChangeTracker.Clear();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
    }
}
