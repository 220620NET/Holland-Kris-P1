using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using sensitive;

namespace DataAccess
{
    public class ConnectionFactory
    {
        private static ConnectionFactory? _instance;
        private readonly static string _connectionString = $"Server=tcp:kserverh.database.windows.net,1433;Initial Catalog=KrisDB;Persist Security Info=False;User ID=sqluser;Password={SensitiveVariables.dbpassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        private ConnectionFactory()
        {
        }

       
    
        public static ConnectionFactory GetInstance()
        {
            // first check if the instance already exists
            // if not create a new one
            if (_instance == null)
            {
                _instance = new ConnectionFactory();
            }
           return _instance;
        }
        public SqlConnection GetConnection()
        {
          // throw new NotImplementedException();
            /* SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;*/

            return new SqlConnection(_connectionString);
        }
    }
}
