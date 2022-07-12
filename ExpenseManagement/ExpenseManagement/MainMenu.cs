using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;
using CustomExceptions;
using DataAccess;

namespace UI
{
    public class MainMenu
    {
        public Users Start()
        {
            
            IUserDAO userDAO = new UserRepository();
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
                            return new FirstScreen().Login(userDAO);
                        }
                        catch (InvalidCredentialsException e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }catch(UsernameNotAvailable e)
                        { 
                            Console.WriteLine(e.Message);
                            break;
                        }
                    case 2:
                        try
                        {
                            return new FirstScreen().Register(userDAO);
                        }
                        catch(UsernameNotAvailable e)
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
        public bool TicketMenu(int selection, Users user)
        {
            return false; // This is simply a placeholder while I get the remainder of this menu working
        }
    }
}
