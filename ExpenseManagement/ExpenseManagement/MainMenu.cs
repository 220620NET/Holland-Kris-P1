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
            Console.WriteLine("Welcome!\nWould you like to...\n1) Login\n2) Register\n3) Exit program");
            int first = (int) new WarningFixer().Parsing();
            FirstScreen firstScreen = new();
            if (first == 1)
            {
                return await firstScreen.Login(api);
            }
            else if (first == 2)
            {
                return await firstScreen.Register(api);
            }else if(first == 3)
            {
                System.Environment.Exit(0);
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
