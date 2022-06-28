namespace Models;
using CustomExceptions;

public enum Status{
    Pending=0,
    Approved=1,Denied=2
}

//Basic class for Tickets setting an Author and Resolver will require its own login
public class Tickets{
    public int Author{get;set;}
    public int ID{get;set;}
    public int Resolver{get;set;}
    public string Description{get;set;}
    public Status State{get;set;}
    public decimal Amount{get;set;}

    public Tickets(int aToSet, int idToSet, string dToSet, decimal money){
        Author = aToSet;
        ID = idToSet;
        Description = dToSet;
        Amount = money;
    }
    public Tickets(int aToSet, int rToSet, int idToSet, string dToSet, decimal money, Status sToSet){
        Author = aToSet;
        ID = idToSet;
        Description = dToSet;
        Amount = money;
        State = sToSet;
        Resolver=rToSet;
    }
    public override string ToString(){
        return $"ID: {this.Author} \nID of Ticket: {this.ID} \nAmount Requested: ${this.Amount}\nReason:{this.Description}";
    }
}