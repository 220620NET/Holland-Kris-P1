using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;

namespace ConsoleFrontEnd
{
    public class MainMenu
    {
        public MainMenu() { }
        private readonly string api = "https://expensemanagementp1.azurewebsites.net/";
        /// <summary>
        /// This will talk to the website to process login and register requests
        /// </summary>
        /// <returns>User who just logged in or Registered</returns>
        public async Task<Users> Start()
        {
            Console.WriteLine("Welcome!\nWould you like to...\n1) Login\n2) Register\n3) Reset your password\n4) Exit program");
            int first = (int) new WarningFixer().Parsing();
            FirstScreen firstScreen = new();
            if (first == 1)
            {
                bool login = true;
                Users you=new();
                while (login)
                {
                    try
                    {
                        you =await firstScreen.Login(api);
                        if (you.userId == 0)
                        {
                            throw new InvalidCredentialsException();
                        }
                        else
                        {
                            login = false;
                        }
                    }
                    catch (InvalidCredentialsException)
                    {
                        Console.WriteLine("Sorry that password does not match the username or you forgot to enter a password or username");
                    }
                    catch (UsernameNotAvailable)
                    {
                        Console.WriteLine("That Username does not exist in the database");
                    }
                }
                return you;
            }
            else if (first == 2)
            {
                while (true)
                {
                    try
                    {
                        return await firstScreen.Register(api);
                    }
                    catch (ResourceNotFoundException)
                    {
                        Console.WriteLine("You didn't enter the right information try that again");
                    }
                    catch (UsernameNotAvailable e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else if (first == 3)
            {
                try
                {
                    return await firstScreen.AlterPassword(api);
                }
                catch (ResourceNotFoundException)
                {
                    Console.WriteLine("That password could not be changed");
                }
            }
            else if (first == 4)
            {
                Environment.Exit(0);
            }
            return new Users();
        }
        /// <summary>
        /// This will talk to the website to handle ticket menu selections for either employees and managers<br/> then process whether the user whiches to continue using the program or log out
        /// </summary>
        /// <param name="you">User who is logged in</param>
        /// <returns>boolean; true if the user wants to continue, false if the user wants to log out</returns>
        public async Task<bool> Selection(Users you)
        {
            if (you.role == Role.Employee)
            {
                await new TicketMenu().ETicket(you,api);
            }
            else
            {
                await new TicketMenu().MTicket(you,api);
            }
            Console.WriteLine("Would you like to do more things? [y/n]");
            string? s = Console.ReadLine();
            while(s == null) 
            {
                Console.WriteLine("I didn't catch that.");
                Console.WriteLine("Would you like to do more things? [y/n]");
                s= Console.ReadLine();
            }           
            char c = s.ToLower()[0];
            if (c == 'y')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }        
}
