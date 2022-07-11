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

        string connectionString = $"Server=tcp:kserverh.database.windows.net,1433;Initial Catalog=KrisDB;Persist Security Info=False;User ID=sqluser;Password={SensitiveVariables.dbpassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
        public void EndConnection()
        {
            new SqlConnection(connectionString).Close();
        }
        public SqlCommand GetInstance(string sql)
        {
            return new SqlCommand(sql,GetConnection());

        }
    }
}
