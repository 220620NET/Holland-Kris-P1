using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;


namespace ConsoleFrontEnd
{
    public class TicketMenu
    {
        public async Task ETicket(Users you,string api)
        {
            Console.WriteLine($"Welcome {you.role} # {you.userId}!\nWhat would you like to do today?\n1) View Tickets\n2) Create a Ticket");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("Would you like those organized in a particular fashion?\n1) Collected by Status\n2) View a single ticket\n3) No Particular collection");
                int sel = int.Parse(Console.ReadLine());
                switch (sel)
                {
                    case 1:
                        Console.WriteLine("What status do you want to see? [Pending, Approved, Denied]");
                        string state = Console.ReadLine();
                       List<Tickets> TicksByStat = await new EmployeeGets().GetTicketsByState(you, state,api);
                        foreach (Tickets t in TicksByStat)
                        {
                            Console.WriteLine(t);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Which ticket would you like to see? [Please enter the ticket id]");
                        int which = int.Parse(Console.ReadLine());
                        Tickets thisOne = await new Gets().GetTicketsByTicketNum(which,api);
                        Console.WriteLine(thisOne);
                        break;
                    case 3:
                        List<Tickets> all = await new EmployeeGets().GetAllTickets(you, api);
                        foreach(Tickets t in all)
                        {
                            Console.WriteLine(t);
                        }
                        break;
                    default:
                        Console.WriteLine("I didn't understand that input.");
                        break;
                }
            }
            else
            {
                
            }
        }
        public async Task MTicket(Users you,string api)
        {

        }
    }
   
}
