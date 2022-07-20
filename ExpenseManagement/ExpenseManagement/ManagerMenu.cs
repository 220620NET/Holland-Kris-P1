using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;

namespace ConsoleFrontEnd
{
    public class ManagerMenu
    {
        public ManagerMenu() { }
        /// <summary>
        /// Allows the manager to view all tickets with a specific status
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">There are no tickets with that status</exception>
        public async Task ViewTicketsByStatus(string api)
        {
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
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// Allows the manager to a ticket with a specific id
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">No such ticket exists</exception>
        public async Task ViewTicketByID(string api)
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
        /// Allows the manager to view all tickets with a specific author
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">That user has not made any tickets</exception>
        public async Task ViewTicketsByAuthor(string api)
        {
            Console.WriteLine("Please enter the id of the author whose tickets you which to view.");
            int id = (int)new WarningFixer().Parsing();
            try
            {
                List<Tickets> tickets = await new Gets().GetTicketsByAuthor(id, api);
                foreach (Tickets t in tickets)
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
        /// Allows the manager to view all tickets 
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">The database is empty</exception>
        public async Task ViewAllTickets(string api)
        {
            try
            {
                List<Tickets> tickets = await new Gets().GetAllTickets(api);
                foreach (Tickets t in tickets)
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
        /// Allows the manager to update a specific ticket
        /// </summary>
        /// <param name="you">The current user</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="InvalidCredentialsException">The update was incomplete</exception>
        /// <exception cref="ResourceNotFoundException">That ticket was already updated</exception>
        public async Task UpdateReimbursement(Users you,string api)
        {
            Console.WriteLine("Which ticket would you like to update? Please enter the ticket number.");
            int thisOne = (int)new WarningFixer().Parsing();
            Console.WriteLine("What do you want to do with the ticket? [Please enter the number for your selection]\n1)Approve\n2)Deny");
            int change = (int)new WarningFixer().Parsing();
            Tickets update = new(thisOne, (Status)change, 1, you.userId, "", 0);
            try
            {
                Tickets good = await new ManagerPosts().UpdateReimbursement(update, api);
                Console.WriteLine("Successfully Updated!");
            }
            catch (InvalidCredentialsException)
            {
                throw new InvalidCredentialsException();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This allows the manager to change a user from employee to manager and vice versa
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="UsernameNotAvailable">That user doesn't exist</exception>
        /// <exception cref="ResourceNotFoundException">Something weird happened</exception>
        public async Task ChangeUser(string api)
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
                throw new UsernameNotAvailable();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This will allow the manager to view a specific user by their id
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">There is no such user</exception>
        public async Task ViewUserByID(string api)
        {
            Console.WriteLine("Please enter the id of the author whose tickets you which to view.");
            int id = (int)new WarningFixer().Parsing();
            try
            {
                Users employee = await new UserGets().GetUser(id, api);
                Console.WriteLine(employee);
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This will allow the manager to view a specific user by their username
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">There is no such user</exception>
        public async Task ViewUserByUsername(string api)
        {
            Console.WriteLine("Please enter the username of the user you which to view.");
            string? s = Console.ReadLine() != null ? Console.ReadLine() : "";
            try
            {
                Users employee = await new UserGets().GetUser(s, api);
                Console.WriteLine(employee);
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This allows the manager to view all users
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="ResourceNotFoundException">The database is empty</exception>
        public async Task ViewUsers(string api)
        {
            try
            {
                List<Users> users = await new UserGets().GetAllUsers(api);
                foreach (Users t in users)
                {
                    Console.WriteLine(t);
                }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
    }
}
