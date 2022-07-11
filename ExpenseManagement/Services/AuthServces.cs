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
                Console.WriteLine(e.Message);
                return new Users();
            }
            
        }

    }
}