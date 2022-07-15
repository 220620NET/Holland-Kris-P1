using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using sensitive;
using Models;
using CustomExceptions;
using System.Data;
using System.Data.SqlTypes;

namespace DataAccess
{
    public class UserRepository: IUserDAO
    {
        private readonly ConnectionFactory _connectionFactory;
        public UserRepository()
        {
            _connectionFactory = ConnectionFactory.GetInstance($"Server=tcp:kserverh.database.windows.net,1433;Initial Catalog=KrisDB;Persist Security Info=False;User ID=sqluser;Password={SensitiveVariables.dbpassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        public UserRepository(ConnectionFactory factory)
        {
            _connectionFactory = factory;
        }
        /// <summary>
        /// This will create an instance of the SQL command SELECT * FROM P1.users;
        /// </summary>
        /// <returns>List of all users in the database</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if either the database does not exist or if the table is empty</exception>
        /// 
        public List<Users> GetAllUsers()
        {
            SqlConnection conn = _connectionFactory.GetConnection();

            conn.Open();
            string sql = "select * from P1.users;";
            SqlCommand command = new SqlCommand(sql, conn);
            List<Users> users = new List<Users>();
            Users s = new Users();
            try
            {
                
                
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int k = s.RoleToNum((string)reader[3]);
                    users.Add(new Users((int)reader[0], (string)reader[1], (string)reader[2],(Role)k));
                }
                reader.Close();
                conn.Close();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            return users;
        }
       
        /// <summary>
        /// This method will create an instance of the SQL command SELECT*FROM P1.users WHERE userID = <input>"userID"</input>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the userId does not exist in the table</exception>
        public Users GetUserById(int userId)
        {
            string sql = "select * from P1.users where userID = @a;";
            //datatype for an active connection
            SqlConnection conn = _connectionFactory.GetConnection();
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@a", userId);
            Users you = new Users();
            Users s = new Users();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (!reader.HasRows)
                {
                    throw new ResourceNotFoundException();
                }
                else
                {
                    you = new Users((int)reader[0], (string)reader[1], (string)reader[2], (Role)s.RoleToNum((string)reader[3]));
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
      
        /// <summary>
        /// This method will create an instance of the SQL command SELECT*FROM P1.users WHERE username=<input>param</input>
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User with specified username</returns>
        /// <exception cref="UsernameNotAvailable">Occurs if the username is not in the database</exception>
        public Users GetUserByUsername(string username)
        {
            string sql = "select * from P1.users where username = @a;";
            //datatype for an active connection
            SqlConnection conn = _connectionFactory.GetConnection();
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql,conn);
            command.Parameters.AddWithValue("@a", username);
            Users you;
            Users s = new Users();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader(); 
                reader.Read();
                if (!reader.HasRows)
                {
                    throw new ResourceNotFoundException();
                }
                else
                {
                    you = new Users((int)reader[0], (string)reader[1], (string)reader[2], (Role)s.RoleToNum((string)reader[3]));
                }
                reader.Close();
                conn.Close();
            }
            catch (UsernameNotAvailable )
            {
                throw new UsernameNotAvailable();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
            return you;
        }
       
        /// <summary>
        /// This method will add a record to the users table in the database by implementing the SQL comment
        ///     insert into P1.users(username, password, role) values(<input.username>,<input.password>, <input.role>);
        ///         As you may have been noticed the role cannot be interpreted as a varchar for the database so it passes through a method to              change the role to a string
        /// This method is inside the Users class called RoleToString()
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>User with provided username</returns>
        /// <exception cref="UsernameNotAvailable">Occurs if username is not found in database</exception>
        public Users CreateUser(Users newUser)
        {
            string sql = "insert into P1.users(username,password, role) values (@u, @p,@r);";
            //datatype for an active connection
            SqlConnection conn = _connectionFactory.GetConnection();
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
                    return GetUserByUsername(newUser.username);
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
            return new Users();
        }
    }
}
