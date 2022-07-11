using Models;
using DataAccess;
using CustomExceptions;
namespace Services
{
    public class AuthServices
    {
        public Users Login(string username, string password)
        {
            Users user;
            try
            {
                user = new UserRepository().GetUserByUsername(username);
                if (user.password == password)
                {
                    return user;
                }
                else { throw new InvalidCredentialsException(); }
            }catch (InvalidCredentialsException e)
            {
                throw;
            }
        }
        public bool Register(Users newUser)
        {
            try
            {
                Users test = new UserRepository().GetUserByUsername(newUser.username);
                if (test.username == newUser.username)
                {
                    throw new UsernameNotAvailable();
                }else
                {
                    return new UserRepository().CreateUser(newUser);
                }
            }catch(Exception)
            {
                throw;
                
            }
            
        }
    }
}