namespace Models;
/*
    Need classes for a Users
        string userID
        string name
        enum Role
        string password
*/

// This enum provides a state for either the employee or manager to have a position
public enum Role{
    Employee,
    Manager
}
public class Users{
    public string userID {get;set;}
    public string name {get;set;}
    public string password{get;set;}
    public Role role{get;set;}


    //Creating a large constructor for all information to be placed in at once
    public Users(string nameToSet, string ID, string passwordToSet, Role role){
        this.name = nameToSet;
        this.userID = ID;
        this.password = passwordToSet;
        this.role = role;
    }
}