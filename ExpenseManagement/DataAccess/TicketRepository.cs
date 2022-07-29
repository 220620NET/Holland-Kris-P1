
using Models;
using CustomExceptions; 

namespace DataAccess
{
    public class TicketRepostitory: ITicketDAO
    {
        //Dependency injection
        private readonly ExpenseDbContext _expenseDbContext;
        public TicketRepostitory(ExpenseDbContext expenseDbContext)
        {
            _expenseDbContext = expenseDbContext;
        }

        /// <summary>
        /// Creates a list of all tickets in the database
        /// </summary>
        /// <returns>List of all tickets</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if database is empty or not found</exception>
        public List<Tickets> GetAllTickets()
        {
                return _expenseDbContext.tickets.ToList()??throw new ResourceNotFoundException();
        }
       
        /// <summary>
        /// Selects all tickets based on the employee id who authored the ticket
        /// </summary>
        /// <param name="author">The userID of the author of a set of tickets</param>
        /// <returns>All tickets authored by the given id</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the author has not generated any tickets</exception>
        public List<Tickets> GetTicketsByAuthor(int author)
        { 
            return _expenseDbContext.tickets.Where(p=>p.author==author).ToList()??throw new ResourceNotFoundException();
        }
        
        /// <summary>
        /// Searches the database for a particualar ticket
        /// </summary>
        /// <param name="TicketNum">The unique identifier of a Ticket</param>
        /// <returns>The specified ticket</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if that ticket does not exist</exception>
        public Tickets GetTicketsById(int TicketNum)
        { 
            return _expenseDbContext.tickets.FirstOrDefault(p => p.ticketNum == TicketNum)??throw new ResourceNotFoundException();
         }
        
        /// <summary>
        /// Searches for ticket of a particular status in the database
        /// </summary>
        /// <param name="state">The state of a ticket{Pending = 0, Approved = 1, Denied = 2}</param>
        /// <returns>List of tickets with a particular status</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if there are no tickets with the specified status</exception>
        public List<Tickets> GetTicketsByStatus(int state)
        {
            return _expenseDbContext.tickets.Where(p => p.status == (Status)state).ToList() ?? throw new ResourceNotFoundException();
        }
        /// <summary>
        /// Generates a new ticket
        /// </summary>
        /// <param name="newTicket">A new ticket with valid information{Author, description, amount}</param>
        /// <returns>boolean stating true if ticket was generated, false otherwise</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the ticket could not be generated</exception>
        public bool CreateTicket(Tickets newTicket)
        { 
            try
            {
                _expenseDbContext.tickets.Add(newTicket);
                _expenseDbContext.SaveChanges();
                _expenseDbContext.ChangeTracker.Clear();
                return true;
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            catch (UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();
            }
            return false;
        }

        /// <summary>
        /// Will update a particular ticket
        /// </summary>
        /// <param name="update">A ticket with the additional parameters that should be updated {Resolver, Status}</param>
        /// <returns>boolean where true if ticket was updated, false otherwise</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if no ticket matches that specification</exception>
        /// <exception cref="UsernameNotAvailable">Occurs if there is no ticket with that number</exception>
        public bool UpdateTicket(Tickets update)
        {
            try
            { 
                if(GetTicketsById(update.ticketNum).status==Status.Approved || GetTicketsById(update.ticketNum).status == Status.Denied)
                {
                    throw new ResourceNotFoundException();
                }
                _expenseDbContext.tickets.Update(update);
                _expenseDbContext.SaveChanges();
                _expenseDbContext.ChangeTracker.Clear();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            catch (UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();
            }
            return false;

        }
        
    }
}
