namespace Models;
public enum Status{
    Pending,Approved,Denied
}

//Basic class for Tickets setting an Author and Resolver will require its own login
public class Tickets{
    public string Author{get;set;}
    public int ID{get;set;}
    public string Resolver{get;set;}
    public string Description{get;set;}
    public Status State{get;set;}
    public decimal Amount{get;set;}
}