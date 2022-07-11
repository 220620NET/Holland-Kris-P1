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
    public class UserRepository:UserDAO
    {
        string connectionString = $"Server=tcp:kserverh.database.windows.net,1433;Initial Catalog=KrisDB;Persist Security Info=False;User ID=sqluser;Password={SensitiveVariables.dbpassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        //GetAllUsers
        public List<Users> GetAllUsers()
        {
            string sql = "select * from P1.users;";
            //datatype for an active connection
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, connection);
            List<Users> users = new List<Users>();
            Users s = new Users();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.RoleToNum((string)reader[3]);
                    users.Add(new Users((int)reader[0], (string)reader[1], (string)reader[2], (Role)k));
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Users>();
            }
            return users;
        }
        public Users GetUserById(int userId)
        {
            string sql = "select * from P1.users where userID = @a;";
            //datatype for an active connection
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@a", userId);
            Users you = new Users();
            Users s = new Users();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.RoleToNum((string)reader[3]);
                    you = new Users((int)reader[0], (string)reader[1], (string)reader[2], (Role)k);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Users();
            }
            return you;
        }
        public Users GetUserByUsername(string username)
        {
            string sql = "select * from P1.users where username = @a;";
            //datatype for an active connection
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@a", username);
            Users you = new Users();
            Users s = new Users();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.RoleToNum((string)reader[3]);
                    you = new Users((int)reader[0], (string)reader[1], (string)reader[2], (Role)k);                   
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Users();
            }
            return you;
        }
        public bool CreateUser(Users newUser)
        {
            string sql = "insert into P1.users(username,password, role) values (@u, @p,@r);";
            //datatype for an active connection
            SqlConnection connection = new SqlConnection(connectionString);
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@u", newUser.username);
            command.Parameters.AddWithValue("@p", newUser.password);
            command.Parameters.AddWithValue("@r", newUser.RoleToString(newUser.role));
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
