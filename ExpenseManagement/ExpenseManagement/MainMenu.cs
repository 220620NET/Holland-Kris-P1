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
        public async Task<Users> Start()
        {
            Console.WriteLine("Welcome!\nWould you like to...\n1) Login\n2) Register");
            Users you;
            int first = int.Parse(Console.ReadLine());
            FirstScreen firstScreen = new FirstScreen();
            if (first == 1)
            {
                return await firstScreen.Login(api);
            }
            else if (first == 2)
            {
                return await firstScreen.Register(api);
            }
            return new Users();
        }
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
            char s = Console.ReadLine().ToLower()[0];
            if (s == 'y')
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
