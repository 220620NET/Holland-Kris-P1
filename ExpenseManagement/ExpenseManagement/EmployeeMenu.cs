using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;


namespace ConsoleFrontEnd
{
    public class EmployeeMenu 
    {
        public EmployeeMenu() { }
        /// <summary>
        /// This allows the employee to see their tickets by status
        /// </summary>
        /// <param name="you">The current user</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">There are no tickets with that status</exception>
        public async Task ViewByStatus(Users you, string api)
        {
            Console.WriteLine("What status do you want to see? [Pending, Approved, Denied]");
            string? state = Console.ReadLine();
            try
            {
                if (state == null)
                {
                    throw new ResourceNotFoundException();
                }
                List<Tickets> TicksByStat = await new EmployeeGets().GetTicketsByState(you, state, api);
                foreach (Tickets t in TicksByStat)
                {
                    Console.WriteLine(t);
                }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This allows the employee to view a ticket based on a specific id
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">There is no ticket with that id</exception>
        public async Task ViewByID(string api)
        {
            Console.WriteLine("Which ticket would you like to see? [Please enter the ticket id]");
            int which = (int)new WarningFixer().Parsing();
            try
            {
                Tickets thisOne = await new Gets().GetTicketsByTicketNum(which, api);
                Console.WriteLine(thisOne);
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This allows an employee to view all of their tickets
        /// </summary>
        /// <param name="you">The current user</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">You have not created any tickets</exception>
        public async Task ViewAll(Users you, string api)
        {
            try
            {
                List<Tickets> all = await new EmployeeGets().GetAllTickets(you, api);
                foreach (Tickets t in all)
                {
                    Console.WriteLine(t);
                }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// Allows the employee to submt a reimbursement ticket
        /// </summary>
        /// <param name="you">The current user</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="InvalidCredentialsException">The information provided is incomplete</exception>
        /// <exception cref="UsernameNotAvailable">There was an error in the processing of that ticket</exception>
        public async Task SubmitReimbursement(Users you, string api)
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
                List<Tickets> createdTicket = await new EmployeePosts().CreateReimbursement(ticketToCreate, api);
                List<Tickets> authors = await new EmployeeGets().GetAllTickets(you, api);
                Console.WriteLine($"Ticket number {authors[authors.Count - 1].ticketNum}");
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
    }
}
