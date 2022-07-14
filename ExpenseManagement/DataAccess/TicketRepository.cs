using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Models;
using CustomExceptions;

namespace DataAccess
{
    public class TicketRepostitory: ITicketDAO
    {
        private readonly ConnectionFactory _connectionFactory;
        public TicketRepostitory(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Creates a list of all tickets in the database
        /// </summary>
        /// <returns>List of all tickets</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if database is empty or not found</exception>
        public List<Tickets> GetAllTickets()
        {
            string sql = "select * from P1.tickets;";
            //datatype for an active connection
            SqlConnection conn = _connectionFactory.GetConnection();
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, conn);
            List<Tickets> tickets = new List<Tickets>();
            Tickets s = new Tickets();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.StateToNum((string)reader[1]);
                    tickets.Add(new Tickets((int)reader[0], (Status) k, (int)reader[2], (int)reader[3], (string)reader[4], (decimal)reader[5]));
                }
                reader.Close();
                conn.Close();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            return tickets;
        }
       
        /// <summary>
        /// Selects all tickets based on the employee id who authored the ticket
        /// </summary>
        /// <param name="author"></param>
        /// <returns>All tickets authored by the given id</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the author has not generated any tickets</exception>
        public List<Tickets> GetTicketsByAuthor(int author)
        {
            string sql = "select * from P1.tickets where author = @a;";
            //datatype for an active connection
            SqlConnection connection = _connectionFactory.GetConnection();  
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@a", author);
            List<Tickets> tickets = new List<Tickets>();
            Tickets s = new Tickets();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.StateToNum((string)reader[1]);
                    tickets.Add(new Tickets((int)reader[0], (Status)k, (int)reader[2], (int)reader[3], (string)reader[4], (decimal)reader[5]));
                }
                reader.Close();
                connection.Close();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            return tickets;
        }
        
        /// <summary>
        /// Searches the database for a particualar ticket
        /// </summary>
        /// <param name="TicketNum"></param>
        /// <returns>The specified ticket</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if that ticket does not exist</exception>
        public Tickets GetTicketsById(int TicketNum)
        {
            string sql = "select * from P1.tickets where ticketNum = @a;";
            //datatype for an active connection
            SqlConnection connection = _connectionFactory.GetConnection();   
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@a", TicketNum);
            Tickets tickets = new Tickets();
            Tickets s = new Tickets();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.StateToNum((string)reader[1]);
                    Tickets ticket =new Tickets ((int)reader[0], (Status)k, (int)reader[2], (int)reader[3], (string)reader[4], (decimal)reader[5]);
                    tickets = ticket;
                }                
                reader.Close();
                connection.Close();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            return tickets;
        }
        
        /// <summary>
        /// Searches for ticket of a particular status in the database
        /// </summary>
        /// <param name="state"></param>
        /// <returns>List of tickets with a particular status</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if there are no tickets with the specified status</exception>
        public List<Tickets> GetTicketsByStatus(Status state)
        {
            string sql = "select * from P1.tickets where status = @a;";
            //datatype for an active connection
            SqlConnection connection = _connectionFactory.GetConnection(); 
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@a", (new Tickets().NumToState((int)state)));
            List<Tickets> tickets = new List<Tickets>();
            Tickets s = new Tickets();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.StateToNum((string)reader[1]);
                    tickets.Add(new Tickets((int)reader[0], (Status)k, (int)reader[2], (int)reader[3], (string)reader[4], (decimal)reader[5]));
                }
                reader.Close();
                connection.Close();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            return tickets;
        }
        /// <summary>
        /// Generates a new ticket
        /// </summary>
        /// <param name="newTicket"></param>
        /// <returns>boolean stating true if ticket was generated, false otherwise</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the ticket could not be generated</exception>
        public bool CreateTicket(Tickets newTicket)
        {
            string sql= "insert into P1.tickets(author, description, amount) values(@ai, @d, @a);";
            //datatype for an active connection
            SqlConnection connection = _connectionFactory.GetConnection();    
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ai", newTicket.author);
            command.Parameters.AddWithValue("d", newTicket.description);
            command.Parameters.AddWithValue("a", newTicket.amount);
            try
            {
                connection.Open();
                int ra = command.ExecuteNonQuery();
                connection.Close();
                if(ra != 0)
                {
                    return true;
                }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            return false;
        }

        //UpdateTicket
        /// <summary>
        /// Will update a particular ticket
        /// </summary>
        /// <param name="update"></param>
        /// <returns>boolean where true if ticket was updated, false otherwise</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if no ticket matches that specification</exception>
        public bool UpdateTicket(Tickets update)
        {
            string sql = "update P1.tickets set status =@s,resolver = @r where ticketNum =@t;";
            SqlConnection connection = _connectionFactory.GetConnection();              //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@s", update.NumToState((int)update.status));
            command.Parameters.AddWithValue("r", update.resolver);
            command.Parameters.AddWithValue("t", update.ticketNum);
            try
            {
                connection.Open();
                int ra = command.ExecuteNonQuery();
                connection.Close();
                if (ra != 0)
                {
                    return true;
                }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            return false;

        }
        
    }
}
