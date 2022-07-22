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
        /// <param name="api">The website url</param>
        /// <returns>The user who logged in or a new </returns>
        /// <exception cref="InvalidCredentialsException">Occurs if the password does not match the entry for the provided username</exception>
        /// <exception cref="UsernameNotAvailable">Occurs if the provided username is not found in the table will suggest to the user to register an account.</exception>
        public async Task<Users> Login(string api)
        {
            Console.WriteLine("What is you username?");
            string? username = Console.ReadLine();
            Console.WriteLine("What is you password");
            string? password = Console.ReadLine();
            Users loginUser;
            if (username == null || password == null)
            {
                throw new InvalidCredentialsException();
            }
            else
            {
                loginUser = new(0,username, password,0);
            }
            try
            {
                return await new AuthPosts().Login(loginUser, api);
            }
            catch (InvalidCredentialsException)
            {
                
                throw new InvalidCredentialsException();
            }
            catch (UsernameNotAvailable)
            {
                
                throw new UsernameNotAvailable();
            } 
        }
        /// <summary>
        /// This will process register requests for the user from the main menu
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns></returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the information provided was invalid</exception>
        /// <exception cref="UsernameNotAvailable">That user already exists</exception>
        public async Task<Users> Register(string api)
        {
            Users newUser = new Users();
            Console.WriteLine("What do you want your username to be?");
            newUser.username = Console.ReadLine();
            Console.WriteLine("What do you want you password to be?");
            newUser.password = Console.ReadLine();
            Console.WriteLine("Are you a Manager?[y/n]");
            string? s = Console.ReadLine();
            if (s == null)
            {
                throw new ResourceNotFoundException();
            }
            else
            {
                char choice = s.ToLower()[0];

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
        /// <summary>
        /// This allows a user to alter their password so long as they know ther id number
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task and the user information</returns>
        /// <exception cref="ResourceNotFoundException">There is no user with that id</exception>
        public async Task<Users> AlterPassword(string api)
        {
            string? s="";
            bool correct = false;
            while (!correct)
            {
                Console.WriteLine("What do you want your neww password to be?");
                s = Console.ReadLine();
                correct = s == null ? false : true;
                
            }
                Console.WriteLine("What is you user ID?");
                Users newUser = new(){
                userId = (int) new WarningFixer().Parsing(),
                password = s
            };
            try
            { 
               return await new AuthPosts().Reset(newUser, api);
               
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
                
        }
    }
}
