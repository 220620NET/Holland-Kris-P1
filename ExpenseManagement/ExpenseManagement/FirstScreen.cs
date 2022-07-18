using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;

namespace ConsoleFrontEnd
{
    public class FirstScreen
    {
       

        /// <summary>
        /// This will process login requests for the user from the main menu
        /// </summary>
        /// <param name="userDAO"></param>
        /// <returns>The user who logged in or a new </returns>
        /// <exception cref="InvalidCredentialsException">Occurs if the password does not match the entry for the provided username</exception>
        /// <exception cref="UsernameNotAvailable">Occurs if the provided username is not found in the table will suggest to the user to register an account.</exception>
        public async Task<Users> Login(string api)
        {
            Console.WriteLine("What is you username?");
            string? username = Console.ReadLine();
            Console.WriteLine("What is you password");
            string? password = Console.ReadLine();
            Users loginUser = new Users(0,username, password,0);
            try
            {
                return await new AuthPosts().Login(loginUser, api);
            }
            catch (InvalidCredentialsException)
            {
                Console.WriteLine("Sorry that password does not match the username or you forgot to enter a password or username");
                throw new InvalidCredentialsException();
            }
            catch (UsernameNotAvailable)
            {
                Console.WriteLine("That Username does not exist in the database");
                throw new UsernameNotAvailable();
            } 
        }
        public async Task<Users> Register(string api)
        {
            Users newUser = new Users();
            Console.WriteLine("What do you want your username to be?");
            newUser.username = Console.ReadLine();
            Console.WriteLine("What do you want you password to be?");
            newUser.password = Console.ReadLine();
            Console.WriteLine("Are you a Manager?[y/n]");
            char choice = Console.ReadLine().ToLower()[0];
            
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
                return await new AuthPosts().Register(newUser, api);
            }
            catch (UsernameNotAvailable)
            { 
                throw new UsernameNotAvailable("An account with that username already exists.");
            } 
        }
    }
}
