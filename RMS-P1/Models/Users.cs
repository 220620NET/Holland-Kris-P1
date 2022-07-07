namespace Models;
using CustomExceptions;

public enum Roles{
    Employee, Manager
}
/*  Users Class
    This is the class that will be used to create, modify and present user information
    This class holds the following information and allows the user to get or set them:
        The userID as an integer 
        The Name of the user as an string
        The Password of the user as an string 
        The Role of the User with an automatic default to "Employee"
    
    There are two constructors
        One for creating an account
        One for creating a login credentials

        This may be simplified to one later because I might want the login to be a different system managed differently
*/
public class Users{
    public Roles role {get;set;}
    public int userID{get;set;}
    public string username{get;set;}
    public string password{get;set;}
    public Users(){}
    public Users(int user, string Name, string password, Roles role){
        userID = user;
        username=Name;
        this.password=password;
        this.role = role;
    }
    public Users(string name, string password){
        username=name;
        this.password=password;
    }
    public override string ToString(){
        return $"Name: {username}, Password: {this.password}";
    }
}
