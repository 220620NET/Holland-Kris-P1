using Models;
using DataAccess;
using CustomExceptions;
namespace Services
{
    public class AuthServices
    {
        /*  Login
         *  This method will take in 2 strings one for a username and one for a password.
         *  It will first Get the user by its username through the UserServices
         *  After the user has been retrieved it checks the username and then the password
         *      If the username of the retrieved user is whitespace it will throw a ResourceNotFound Exception
         *      If the password is not the same as the retrieved user if will throw an InvalidCredentials Exception
         */
        public Users Login(string username, string password)
        {
            Users user;
            try
            {
                user = new UserServices().GetUserByUsername(username);
                if (user.username == "")
                {
                    throw new ResourceNotFoundException;
                }
                if (user.password == password)
                {
                    return user;
                }
                else { throw new InvalidCredentialsException(); }
            }
            catch (ResourceNotFoundException)
            {
                throw;
            }
            catch (InvalidCredentialsException e)
            {
                throw;
            }


        }
        public bool Register(Users newUser)
        {
            try
            {
                Users test = new UserServices().GetUserByUsername(newUser.username);
                if (test.username == newUser.username)
                {
                    throw new UsernameNotAvailable();
                }else
                {
                    return new UserRepository().CreateUser(newUser);
                }
            }catch(UsernameNotAvailable)
            {
                throw;
                
            }
            
        }
    }
}