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
                        try
                        {
                            List<Tickets> TicksByStat = await new EmployeeGets().GetTicketsByState(you, state,api);
                            foreach (Tickets t in TicksByStat)
                            {
                                Console.WriteLine(t);
                            }
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("There are no tickets with that state that you have authored.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Which ticket would you like to see? [Please enter the ticket id]");
                        int which = int.Parse(Console.ReadLine());
                        try
                        {
                            Tickets thisOne = await new Gets().GetTicketsByTicketNum(which,api);
                            Console.WriteLine(thisOne);
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("There is no ticket with that ID.");
                        }
                        break;
                    case 3:
                        try
                        {
                            List<Tickets> all = await new EmployeeGets().GetAllTickets(you, api);
                            foreach(Tickets t in all)
                            {
                                Console.WriteLine(t);
                            }
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("You have not made any tickets.");
                        }
                        break;
                    default:
                        Console.WriteLine("I didn't understand that input.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("How much would you like to make this reimbursement out for");
                Tickets ticketToCreate = new Tickets(0,Status.Pending,you.userId,2,"",0);
                ticketToCreate.amount = decimal.Parse(Console.ReadLine());
                Console.WriteLine($"Why are you requesting {ticketToCreate.amount}? ");
                ticketToCreate.description = Console.ReadLine();
                ticketToCreate.author = you.userId;
                try
                {
                    List<Tickets> createdTicket = await new EmployeePosts().CreateReimbursement(ticketToCreate,api);
                    List<Tickets> authors = await new EmployeeGets().GetAllTickets(you, api);
                    Console.WriteLine($"Ticket number {authors[authors.Count() - 1].ticketNum}");
                }
                catch (InvalidCredentialsException)
                {
                    Console.WriteLine("You didn't enter in the right information.");
                }
                catch (UsernameNotAvailable)
                {
                    Console.WriteLine("Something weird just happened.");
                }
            }
        }
        public async Task MTicket(Users you,string api)
        {
            Console.WriteLine($"Welcome {you.username}!\nWhat would you like to do today?\n 1) View Tickets\n2) Update a ticket");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("Would you like those organized in a particular fashion?\n1) Collected by Status\n2) View a single ticket\n3) From a particular associate\n4) No particular order");
                int sel = int.Parse(Console.ReadLine());
                switch (sel)
                {
                    case 1:
                        Console.WriteLine("What status do you want to see? [Pending, Approved, Denied]");
                        string state = Console.ReadLine();
                        try
                        {
                            List<Tickets> TicksByStat = await new Gets().GetTicketsByState(state, api);
                            foreach (Tickets t in TicksByStat)
                            {
                                Console.WriteLine(t);
                            }
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("There are no tickets with that state that you have authored.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Which ticket would you like to see? [Please enter the ticket id]");
                        int which = int.Parse(Console.ReadLine());
                        try
                        {
                            Tickets thisOne = await new Gets().GetTicketsByTicketNum(which, api);
                            Console.WriteLine(thisOne);
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("There is no ticket with that ID.");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Please enter the id of the author whose tickets you which to view.");
                        int id = int.Parse(Console.ReadLine());
                        try
                        {
                            List<Tickets> tickets = await new Gets().GetTicketsByAuthor(id, api);
                            foreach(Tickets t in tickets)
                            {
                                Console.WriteLine(t);
                            }
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("That associate has not made any tickets yet.");
                        }
                        break;
                    case 4:
                        try
                        {
                            List<Tickets> tickets = await new Gets().GetAllTickets(api);
                            foreach(Tickets t in tickets)
                            {
                                Console.WriteLine(t);
                            }
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("Something went wrong.");
                        }
                        break;
                    default:
                        Console.WriteLine("I did not understand your request please enter a number from the list.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Which ticket would you like to update? Please enter the ticket number.");
                int thisOne = int.Parse(Console.ReadLine());
                Console.WriteLine("What do you want to do with the ticket? [Please enter the number for your selection]\n1)Approve\n2)Deny");
                int change = int.Parse(Console.ReadLine());
                Tickets update = new Tickets(thisOne, (Status)change, 1, you.userId, "", 0);
                try
                {
                    Tickets good = await new ManagerPosts().UpdateReimbursement(update,api);
                    Console.WriteLine("Successfully Updated!");
                }
                catch (InvalidCredentialsException)
                {
                    Console.WriteLine("Somethign wasn't input properly");
                }
                catch (UsernameNotAvailable)
                {
                    Console.WriteLine("That ticket has already been updated");
                }
            }
        }
    }
   
}
