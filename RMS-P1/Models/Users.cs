namespace Models;
using CustomExceptions;

public enum Roles{
    Employee, Manager
}
public class Users{
    public Roles Role {get;set;}
    public int UserID{get;set;}
    public string Name{get;set;}
    public string Password{get;set;}

    public Users(int user, string Name, string password, Roles role){
        UserID = user;
        this.Name=Name;
        Password=password;
        Role = role;
    }
    public Users(string name, string password){
        Name=name;
        Password=password;
    }
    public override string ToString(){
        return $"Name: {Name}, Password: {Password}";
    }
}
