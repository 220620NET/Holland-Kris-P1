using Models;
namespace DataAccess
{
    public interface TicketDAO
    {
        public List<Tickets> GetAllTickets();
        public bool CreateTicket(Tickets newTicket);
        public bool UpdateTicket(Tickets newTicket);
        public Tickets GetTicketsById(int ticketNum);
        public List<Tickets> GetTicketsByAuthor(int authorId);
        public List<Tickets> GetTicketsByStatus(Status state);
    }
    public interface UserDAO
    {
        public List<Users> GetAllUsers();
        public Users GetUserById(int userId);
        public Users GetUserByUsername(string userName);
        public bool CreateUser(Users user);
    }
}