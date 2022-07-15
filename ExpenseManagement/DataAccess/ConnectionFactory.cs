using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using sensitive;

namespace DataAccess
{

    //This clas uses Singleton and factory design pattern
    public class ConnectionFactory
    {
        //Dependency Injection
        private static ConnectionFactory? _instance;
        private readonly string _connectionString;

        private ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }


        /// <summary>
        /// getter for the one and only instance of connection factory
        /// </summary>
        /// <param name="connectionString">The connection to the server</param>
        /// <returns>An instance of the connection string</returns>
        public static ConnectionFactory GetInstance(string connectionString)
        {
            //first check if the instance already exists
            //if not, create a new one and assign to our private field
            if (_instance == null)
            {
                _instance = new ConnectionFactory(connectionString);
            }
            //if it already exists, just give that instance
            return _instance;
        }
        /// <summary>
        /// Getter for the connection
        /// </summary>
        /// <returns>The connection to the server</returns>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
 }
