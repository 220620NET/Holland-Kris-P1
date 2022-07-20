using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;


namespace ConsoleFrontEnd
{
    public class TicketMenu
    {
        /// <summary>
        /// This will talk with the website and will operate as a selection screen for the employee
        /// </summary>
        /// <param name="you">The currect User</param>
        /// <param name="api">The url for the website</param>
        /// <returns></returns>
        public async Task ETicket(Users you,string api)
        {
            Console.WriteLine($"Welcome {you.role} # {you.userId}!\nWhat would you like to do today?\n1) View Tickets\n2) Create a Ticket");
            if ((int)new WarningFixer().Parsing() == 1)
            {
                Console.WriteLine("Would you like those organized in a particular fashion?\n1) Collected by Status\n2) View a single ticket\n3) No Particular collection");
                int sel = (int) new WarningFixer().Parsing();
                switch (sel)
                {
                    case 1:
                        Console.WriteLine("What status do you want to see? [Pending, Approved, Denied]");
                        string? state = Console.ReadLine();
                        try
                        {
                            if (state == null)
                            {
                                throw new ResourceNotFoundException();
                            }
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
                        int which = (int)new WarningFixer().Parsing();
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
                Tickets ticketToCreate = new(0, Status.Pending, you.userId, 2, "", 0)
                {
                    amount = new WarningFixer().Parsing()
                };
                Console.WriteLine($"Why are you requesting {ticketToCreate.amount}? ");
                ticketToCreate.description = Console.ReadLine();
                ticketToCreate.author = you.userId;
                try
                {
                    List<Tickets> createdTicket = await new EmployeePosts().CreateReimbursement(ticketToCreate,api);
                    List<Tickets> authors = await new EmployeeGets().GetAllTickets(you, api);
                    Console.WriteLine($"Ticket number {authors[authors.Count - 1].ticketNum}");
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
        /// <summary>
        /// This will talk with the website and will operate as a selection screen for the manager
        /// </summary>
        /// <param name="you">The current user</param>
        /// <param name="api">The website url</param>
        /// <returns></returns>
        public async Task MTicket(Users you,string api)
        {
            Console.WriteLine($"Welcome {you.username}!\nWhat would you like to do today?\n1) View Tickets\n2) Update a ticket\n3) View Users\n4) Change a current users role");
            int sim = (int)new WarningFixer().Parsing();
            if (sim== 1)
            {
                Console.WriteLine("Would you like those organized in a particular fashion?\n1) Collected by Status\n2) View a single ticket\n3) From a particular associate\n4) No particular order");
                int sel = (int)new WarningFixer().Parsing();
                switch (sel)
                {
                    case 1:
                        Console.WriteLine("What status do you want to see? [Pending, Approved, Denied]");
                        string? state = Console.ReadLine();
                        try
                        {
                            if (state == null)
                            {
                                throw new ResourceNotFoundException();
                            }
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
                        int which = (int)new WarningFixer().Parsing();
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
                        int id = (int)new WarningFixer().Parsing();
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
            else if (sim== 2)
            {
                Console.WriteLine("Which ticket would you like to update? Please enter the ticket number.");
                int thisOne = (int)new WarningFixer().Parsing();
                Console.WriteLine("What do you want to do with the ticket? [Please enter the number for your selection]\n1)Approve\n2)Deny");
                int change = (int)new WarningFixer().Parsing();
                Tickets update = new(thisOne, (Status)change, 1, you.userId, "", 0);
                try
                {
                    Tickets good = await new ManagerPosts().UpdateReimbursement(update,api);
                    Console.WriteLine("Successfully Updated!");
                }
                catch (InvalidCredentialsException)
                {
                    Console.WriteLine("Somethign wasn't input properly");
                }
                catch (ResourceNotFoundException)
                {
                    Console.WriteLine("That ticket has already been updated");
                }
            }else if (sim== 4)
            {
                Console.WriteLine("Which user would you like to update? Please enter the userId.");
                int thisOne = (int)new WarningFixer().Parsing();
                Console.WriteLine("Would you like to make them an empoyee or manager? [Please enter the number for your selection]\n0)Employee\n1)Manager");
                int change = (int)new WarningFixer().Parsing();
                Users update = new()
                {
                    userId = thisOne,
                    role = (Role)change
                };
                try
                {
                    await new AuthPosts().Payroll(update, api);
                    Console.WriteLine("Successfully Updated!");
                }
                catch (UsernameNotAvailable)
                {
                    Console.WriteLine("Somethign wasn't input properly");
                }
                catch (ResourceNotFoundException)
                {
                    Console.WriteLine("That ticket has already been updated");
                }

            }
            else
            {
                Console.WriteLine("Which selection of users would you like to see:\n1) All Users\n2) Specific User By their username\n3) Specific User by their userID");
                int sel = (int)new WarningFixer().Parsing();
                switch (sel)
                {
                    case 2:
                        Console.WriteLine("Please enter the username of the user you which to view.");
                        string? s  = Console.ReadLine()!=null?Console.ReadLine():"";
                        try
                        {
                            Users employee = await new UserGets().GetUser(s, api);
                            Console.WriteLine(employee + "Password: " + employee.password);
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("That associate has not made any tickets yet.");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Please enter the id of the author whose tickets you which to view.");
                        int id = (int)new WarningFixer().Parsing();
                        try
                        {
                            Users employee = await new UserGets().GetUser(id, api);
                            Console.WriteLine(employee+"Password: "+employee.password);
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("That associate has not made any tickets yet.");
                        }
                        break;
                    default: 
                        try
                        {
                            List<Users> users = await new UserGets().GetAllUsers(api);
                            foreach (Users t in users)
                            {
                                Console.WriteLine(t + "Password: " + t.password);
                            }
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("That associate has not made any tickets yet.");
                        }
                        break;
                }
            }
        }
    }
   
}
