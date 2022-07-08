using Models;
using System.Data.SqlClient;
using Sensitive;
/* 
    What is the DAO layer for?
        - the DAO layer's purpose is to execute the SQL statements to the database
*/
namespace DataAccess;
public class TodoDAOImpl : TodoDAO{
    // we need the connection string from azure
    string connectionString=$"Server=tcp:kserverh.database.windows.net,1433;Initial Catalog=KrisDB;Persist Security Info=False;User ID=sqluser;Password={SensitiveVariable.dbpassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
    public List<Todo> GetAllTodos(){
         //this defines the sql statement we wish to do
        string sql ="select * from todoapp.todos";
        //datatype for an active connection
        SqlConnection connection = new SqlConnection(connectionString);
        //datatype to reference the sql command you want to do to a specific connection
        SqlCommand command= new SqlCommand(sql,connection);
        List<Todo> Task = new List<Todo>();
        try{
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read()){
                Task.Add(new Todo((int)reader[0],(string)reader[1],(bool)reader[2]));
            }
            reader.Close();
        }catch(Exception e){
            Console.WriteLine(e.Message);
            return new List<Todo>();
        }
        return Task;
    }
    public bool CreateTodo(Todo todo){
        string sql ="insert into todoapp.todos(description) values (@description);";
        //datatype for an active connection
        SqlConnection connection = new SqlConnection(connectionString);
        //datatype to reference the sql command you want to do to a specific connection
        SqlCommand command= new SqlCommand(sql,connection);
        command.Parameters.AddWithValue("@description", todo.description);
        List<Todo> Task = new List<Todo>();
        try{
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            if(rowsAffected!=0){
                return true;
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
            return false;
        }
        return false;
    }
    public void DeleteOneTodo(int todoId){
        string sql ="delete from todoapp.todos where id=@id;";
        //datatype for an active connection
        SqlConnection connection = new SqlConnection(connectionString);
        //datatype to reference the sql command you want to do to a specific connection
        SqlCommand command= new SqlCommand(sql,connection);
        command.Parameters.AddWithValue("@id", todoId);
        try{
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }

}
