using Models;
namespace DataAccess
{
    /// <summary>
    /// Interface for the Ticket Repository class
    /// </summary>
    public interface ITicketDAO
    {
        public List<Tickets> GetAllTickets();
        public bool CreateTicket(Tickets newTicket);
        public bool UpdateTicket(Tickets newTicket);
        public Tickets GetTicketsById(int ticketNum);
        public List<Tickets> GetTicketsByAuthor(int authorId);
        public List<Tickets> GetTicketsByStatus(int state);
    }
    /// <summary>
    /// Interface for the User Repository class
    /// </summary>
    public interface IUserDAO
    {
        public List<Users> GetAllUsers();
        public Users GetUserById(int? userId);
        public Users GetUserByUsername(string? userName);
        public Users CreateUser(Users user);
    }
}