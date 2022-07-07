using Models;
using System.Text.Json;

namespace DataAccess;
/*  UserRegistry Class
    This class holds methods to read and search for a User
    This class can also add Users and will print all Users to a json file when done.
*/
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
    /* GetUserByID
        Will attempt to locate a user with a given ID by implementing the GetUsers() Method
            If no such user exists it will throw an exception to the user
            Otherwise it returns the user to the user
    */
    public Users GetUserByID(int ID){
        try{
            Dictionary<int,Users> user = GetUsers();
            return user[ID];
        }catch(JsonException){
            throw;
        }
    } 
    /*  AddUser
        This method will add a User to the dictionary of users and will inform the user if an error occurs
    */
    public Users AddUser(Users newUser){
        try{
            Dictionary<int,Users> allUsers=GetUsers();
            allUsers.Add(newUser.userID, newUser);
            File.WriteAllText(file, JsonSerializer.Serialize(allUsers));
            return newUser;
        }catch(JsonException){
            throw;
        }
    }

}
