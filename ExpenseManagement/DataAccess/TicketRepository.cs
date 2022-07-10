using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using sensitive;
using Models;

namespace DataAccess
{
    public class TicketRepostitory:TicketDAO
    {
        string connectionString = $"Server=tcp:kserverh.database.windows.net,1433;Initial Catalog=KrisDB;Persist Security Info=False;User ID=sqluser;Password={SensitiveVariables.dbpassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        //GetAllTickets
        public List<Tickets> GetAllTickets()
        {
            string sql = "select * from P1.tickets;";
            //datatype for an active connection
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, connection);
            List<Tickets> tickets = new List<Tickets>();
            Tickets s = new Tickets();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.StateToNum((string)reader[1]);
                    tickets.Add(new Tickets((int)reader[0], (Status) k, (int)reader[2], (int)reader[3], (string)reader[4], (decimal)reader[5]));
                }
                reader.Close();
                connection.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Tickets>();
            }
            return tickets;
        }
        //GetTicketsByAuthor
        public List<Tickets> GetTicketsByAuthor(int author)
        {
            string sql = "select * from P1.tickets where author = @a;";
            //datatype for an active connection
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Tickets>();
            }
            return tickets;
        }
        //GetTicketById
        public Tickets GetTicketsById(int TicketNum)
        {
            string sql = "select * from P1.tickets where ticketNum = @a;";
            //datatype for an active connection
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Tickets();
            }
            return tickets;
        }
        //GetTicketsByStatus
        public List<Tickets> GetTicketsByStatus(Status state)
        {
            string sql = "select * from P1.tickets where status = @a;";
            //datatype for an active connection
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Tickets>();
            }
            return tickets;
        }
        public bool CreateTicket(Tickets newTicket)
        {
            string sql= "insert into P1.tickets(author, description, amount) values(@ai, @d, @a);";
            //datatype for an active connection
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        //UpdateTicket
        public bool UpdateTicket(Tickets update)
        {
            string sql = "update P1.tickets set status =@s,resolver = @r where ticketNum =@t;";
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;

        }
        
    }
}
