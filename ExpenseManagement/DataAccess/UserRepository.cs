using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using sensitive;
using Models;
using CustomExceptions;

namespace DataAccess
{
    public class UserRepository: IUserDAO
    {
       /* string connectionString = $"Server=tcp:kserverh.database.windows.net,1433;Initial Catalog=KrisDB;Persist Security Info=False;User ID=sqluser;Password={SensitiveVariables.dbpassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";*/

        /* GetAllUsers Method
         * 
         *  This will create an instance of the SQL command SELECT * FROM P1.users;
         *  This method will return the complete list of users in the database.
         *      Should the database be empty it will return a new instance of a List of Users
         *  Since the role of the user is saved as an enumerator it must be transfered to a number to indicate a enumeration
         *  This is completed by callng a RoleToNum method in the Users class
         */
        public List<Users> GetAllUsers()
        {
            SqlConnection conn = ConnectionFactory.GetInstance().GetConnection();
            string sql = "select * from P1.users;";
            //datatype for an active connection
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, conn);
            List<Users> users = new List<Users>();
            Users s = new Users();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.RoleToNum((string)reader[3]);
                    users.Add(new Users((int)reader[0], (string)reader[1], (string)reader[2], (Role)k));
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Users>();
            }
            return users;
        }
        /*  GetUserById
         *  This method will create an instance of the SQL command SELECT*FROM P1.users WHERE userID=<input>;
         *      To achieve this it will implement the SqlCommand.Parameters.AddWithValue() Method
         *  Since the role of the user is saved as an enumerator it must be transfered to a number to indicate a enumeration
         *  This is completed by callng a RoleToNum method in the Users class
         */
        public Users GetUserById(int userId)
        {
            string sql = "select * from P1.users where userID = @a;";
            //datatype for an active connection
            SqlConnection conn = ConnectionFactory.GetInstance().GetConnection();
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@a", userId);
            Users you = new Users();
            Users s = new Users();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.RoleToNum((string)reader[3]);
                    you = new Users((int)reader[0], (string)reader[1], (string)reader[2], (Role)k);
                }
                reader.Close();
                conn.Close();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            return you;
        }
        /*  GetUserByUsername
         *  This method will create an instance of the SQL command SELECT*FROM P1.users WHERE username=<input>;
         *      To achieve this it will implement the SqlCommand.Parameters.AddWithValue() Method
         *  Since the role of the user is saved as an enumerator it must be transfered to a number to indicate a enumeration
         *  This is completed by callng a RoleToNum method in the Users class
         */
        public Users GetUserByUsername(string username)
        {
            string sql = "select * from P1.users where username = @a;";
            //datatype for an active connection
            SqlConnection conn = ConnectionFactory.GetInstance().GetConnection();
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql,conn);
            command.Parameters.AddWithValue("@a", username);
            Users you = new Users();
            Users s = new Users();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.RoleToNum((string)reader[3]);
                    you = new Users((int)reader[0], (string)reader[1], (string)reader[2], (Role)k);                   
                }

                reader.Close();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Users();
            }
            return you;
        }
        /*  CreateUser
         *  This method will add a record to the users table in the database by implementing the SQL command
         *      insert into P1.users(username, password, role) values(<input.username>,<input.password>, <input.role>);
         *  As may have been noticed the role cannot be interpreted as a varchar for the database so it passes through a method to change the role to a string
         *  This method is inside the Users class called RoleToString()
         *  This method finally returns a boolean indicating if the user was added
         *  If the user was not created it throws an exception that the username was not available and returns false. This is because this is the only reason a normal request would be not allowed.
         */
        public bool CreateUser(Users newUser)
        {
            string sql = "insert into P1.users(username,password, role) values (@u, @p,@r);";
            //datatype for an active connection
            SqlConnection conn = ConnectionFactory.GetInstance().GetConnection();
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql,conn);
            command.Parameters.AddWithValue("@u", newUser.username);
            command.Parameters.AddWithValue("@p", newUser.password);
            command.Parameters.AddWithValue("@r", newUser.RoleToString(newUser.role));
            try
            {
                conn.Open();
                int ra = command.ExecuteNonQuery();
                conn.Close();
                if (ra != 0)
                {
                    return true;
                }
                else
                {
                    throw new UsernameNotAvailable();
                }
            }
            catch (UsernameNotAvailable e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
