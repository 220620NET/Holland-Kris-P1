using System.Data.SqlClient;
using sensitive;
using Models;
using CustomExceptions; 

namespace DataAccess
{
    public class UserRepository: IUserDAO
    {
        //Dependency injection
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
                    users.Add(new Users((int)reader[0], (string)reader[1], (string)reader[2],k));
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
        /// <param name="userId">A valid userID</param>
        /// <returns></returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the userId does not exist in the table</exception>
        public Users GetUserById(int? userId)
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
                    you = new Users((int)reader[0], (string)reader[1], (string)reader[2], s.RoleToNum((string)reader[3]));
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
        /// <param name="username">A valid username</param>
        /// <returns>User with specified username</returns>
        /// <exception cref="UsernameNotAvailable">Occurs if the username is not valid</exception>
        /// <exception cref="ResourceNotFoundException">Occurs if the username is not in the database</exception>
        public Users GetUserByUsername(string? username)
        {
            string sql = "select * from P1.users where username = @a;";
            //datatype for an active connection
            SqlConnection conn = _connectionFactory.GetConnection();
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql,conn);
            username = username != null ? username : "";
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
                    you = new Users((int)reader[0], (string)reader[1], (string)reader[2], s.RoleToNum((string)reader[3]));
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
        /// Method to create a new user replicating the insert into DML command
        /// </summary>
        /// <param name="newUser">A new user with no specified userID</param>
        /// <returns>The user after being created</returns>
        /// <exception cref="UsernameNotAvailable"></exception>
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
                    if (newUser.username != null)
                    {
                        return GetUserByUsername(newUser.username);
                    }
                    else
                    {
                        throw new UsernameNotAvailable();
                    }
                }
                else
                {
                    throw new UsernameNotAvailable();
                }
            }
            catch (UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();
            } 
        }
        /// <summary>
        /// This represents the update sql command for changing the password of a given user id
        /// </summary>
        /// <param name="user">The user to change passwords of</param>
        /// <exception cref="ResourceNotFoundException">That user doesn't exist</exception>
        public void ResetPassword(Users user)
        {
            string sql = "update P1.users set password = @p where userID = @i;";
            //datatype for an active connection
            SqlConnection conn = _connectionFactory.GetConnection();
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@i", user.userId);
            command.Parameters.AddWithValue("@p", user.password);
            try
            {
                conn.Open();
                int ra = command.ExecuteNonQuery();
                conn.Close();
                
                if (ra==0)
                {
                    throw new ResourceNotFoundException();
                }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This represents the update sql command to change the role of a given user id
        /// </summary>
        /// <param name="user">The user to change and their role</param>
        /// <exception cref="ResourceNotFoundException">That user doesn't exist</exception>
        public void PayRollChange(Users user)
        {
            string sql = "update P1.users set role = @p where userID = @i;";
            //datatype for an active connection
            SqlConnection conn = _connectionFactory.GetConnection();
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@i", user.userId);
            command.Parameters.AddWithValue("@p", user.role);
            try
            {
                conn.Open();
                int ra = command.ExecuteNonQuery();
                conn.Close();

                if (ra == 0)
                {
                    throw new ResourceNotFoundException();
                }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        public void DeleteUser(int id)
        {
            string sql = "delete from P1.users where userID = @i;";
            //datatype for an active connection
            SqlConnection conn = _connectionFactory.GetConnection();
            //datatype to reference the sql command you want to do to a specific connection
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@i", id);
            try
            {
                conn.Open();
                int ra = command.ExecuteNonQuery();
                conn.Close();

                if (ra == 0)
                {
                    throw new ResourceNotFoundException();
                }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
    }
}
