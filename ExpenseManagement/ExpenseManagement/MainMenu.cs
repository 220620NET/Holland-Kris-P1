
using Models;
using Services;
using CustomExceptions;

namespace UI
{
    public class MainMenu
    {
        private readonly AuthServices _authServices;
        public MainMenu(AuthServices authServices)
        {
            _authServices = authServices;
        }

        public Users Start()
        {           
            return Entrance();
        }
        public int Selection(Users user)
        {
            if (user.role == Role.Employee)
            {
               return new SecondScreen().Employee(user);
            }
            else
            {
                return new SecondScreen().Manager(user);
            }
        }
        public int TicketMenu(int selection, Users user)
        {
            if (user.role == Role.Employee)
            {
                new IntenseMenu().ETickets(selection, user);
            }
            else
            {
                new IntenseMenu().MTickets(selection, user);
            }
            Console.WriteLine("Do you want to...\n1) View or manipulate other tickets\n2) Exit the program.");
            return int.Parse(Console.ReadLine());
        }
        private Users Entrance()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Welcome to the EMS!\nWould you like to [Press either 1 or 2]:\n1) Login\n2) Register");
                 int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        try
                        {
                            return new FirstScreen(_authServices).Login();
                        }
                        catch (InvalidCredentialsException e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }
                        catch (UsernameNotAvailable e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }
                    case 2:
                        try
                        {
                            return new FirstScreen(_authServices).Register();
                        }
                        catch (UsernameNotAvailable e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }
                    default:
                        Console.WriteLine("I did not understand your input.");
                        break;
                }
            }
            throw new ResourceNotFoundException();
        }
        public int Parsing()
        {
            int k = 0;
            while (k == 0)
            {
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("That wasn't a number");
                    k=0;
                }
            }
            return 0;
        }
    }
}
