using Models;
using DataAccess;
using CustomExceptions;
namespace Services
{
    public class AuthServices
    {
        private readonly IUserDAO _user;
        // Dependency Injection
        public AuthServices(IUserDAO userDao)
        {
            _user = userDao;
        }
       
        /// <summary>
        /// This methods logs in a users with the username and password and searches for the user in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>User record from the database concurrent with the provided inputs</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the username does not exist in the database</exception>
        /// <exception cref="InvalidCredentialsException">Occurs if the username and password do not match</exception>
        public Users Login(string username, string password)
        {
            Users user;
            try
            {
                user = _user.GetUserByUsername(username);
                if (user.username == "")
                {
                    throw new ResourceNotFoundException();
                }
                if (user.password == password)
                {
                    return user;
                }
                else { throw new InvalidCredentialsException(); }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            catch (InvalidCredentialsException)
            {
                throw new InvalidCredentialsException();
            }
        }
        /// <summary>
        /// Registers a new user and simultaneosly logs in the new user after successful registration
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>Registered new user</returns>
        /// <exception cref="UsernameNotAvailable">Occurs if the provided username already exists in the database or if the username is harmful</exception>
        public Users Register(Users newUser)
        {
            try
            {
                Users test = _user.GetUserByUsername(newUser.username);
                if (test.username == newUser.username)
                {
                    throw new UsernameNotAvailable();
                }
                else if (newUser.username == "" || newUser.username.Contains(";") || newUser.username.Contains("drop"))
                {
                    throw new UsernameNotAvailable();
                }
                else
                {
                    Users user = _user.CreateUser(newUser);
                    return user;
                }
            }catch(UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();                
            }            
        }
    }
}