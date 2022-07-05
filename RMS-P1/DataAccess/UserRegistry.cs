using Models;
using System.Text.Json;

namespace DataAccess;

public class UserRegistry{
    private const string file = "../DataAccess/userRegistry.json";
    
    /* Get Users
        Will attempt to read a Json file and provide all detatils
        If the Json file is empty it will return a new Dictionary
    */
    public Dictionary<int, Users> GetUsers(){
        string fs = File.ReadAllText(file);
        try{
            return JsonSerializer.Deserialize<Dictionary<int, Users>>(fs);
        }catch(JsonException){

            return new Dictionary<int, Users>();
        }
    }

    public Users GetUserByID(int ID){
        try{
            Dictionary<int,Users> user = GetUsers();
            return user[ID];
        }catch(JsonException){
            throw;
        }
    } 
    public Users AddUser(Users newUser){
        try{
            Dictionary<int,Users> allUsers=GetUsers();
            allUsers.Add(newUser.UserID, newUser);
            File.WriteAllText(file, JsonSerializer.Serialize(allUsers));
            return newUser;
        }catch(JsonException){
            throw;
        }
    }

}
