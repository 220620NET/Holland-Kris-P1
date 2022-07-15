using Services;
using CustomExceptions;
using Models;

namespace UI
{
    public class FirstScreen
    {
        private readonly AuthServices _auth;
        public FirstScreen(AuthServices auth)
        {
            _auth = auth;
        }

        /// <summary>
        /// This will process login requests for the user from the main menu
        /// </summary>
        /// <param name="userDAO"></param>
        /// <returns>The user who logged in or a new </returns>
        /// <exception cref="InvalidCredentialsException">Occurs if the password does not match the entry for the provided username</exception>
        /// <exception cref="UsernameNotAvailable">Occurs if the provided username is not found in the table will suggest to the user to register an account.</exception>
        public Users Login()
        {
            Console.WriteLine("What is you username?");
            string? username = Console.ReadLine();
            Console.WriteLine("What is you password");
            string? password = Console.ReadLine();
            try
            {
                if (username == null || password == null) 
                {
                    throw new InvalidCredentialsException();
                }
               return _auth.Login(username, password);
            }
            catch (InvalidCredentialsException)
            {
                throw new InvalidCredentialsException("Sorry that password does not match the username or you forgot to enter a password or username");
            }
            catch (UsernameNotAvailable)
            {
                throw new UsernameNotAvailable("That Username does not exist in the database");
            } 
        }
        public Users Register()
        {
            Users newUser = new Users();
            Console.WriteLine("What do you want your username to be?");
            newUser.username = Console.ReadLine();
            Console.WriteLine("What do you want you password to be?");
            newUser.password = Console.ReadLine();
            Console.WriteLine("Are you a Manager?[y/n]");
            char choice = Console.ReadLine()[0];
            Users you2;
            if (choice == 'y' || choice == 'Y')
            {
                newUser.role = Role.Manager;
            }
            else
            {
                newUser.role = Role.Employee;
            }
            try
            {
                return _auth.Register(newUser);
            }
            catch (UsernameNotAvailable)
            { 
                throw new UsernameNotAvailable("An account with that username already exists.");
            } 
        }
    }
}
