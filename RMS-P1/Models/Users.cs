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
}
