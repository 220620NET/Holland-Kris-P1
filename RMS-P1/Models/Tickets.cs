namespace Models;
public enum Status{
    Pending,Approved,Denied
}
public class Tickets{
    public string Author{get;set;}
    public int ID{get;set;}
    public string Resolver{get;set;}
    public string Description{get;set;}
    public Status state{get;set;}
    public decimal amount{get;set;}
}