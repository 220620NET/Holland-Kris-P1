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
    private decimal Amount{get;set;}
    //For the Employee making the ticket
    public Tickets(int aToSet, int idToSet, string dToSet, decimal money){
        Author = aToSet;
        ID = idToSet;
        Description = dToSet;
        this.Amount = money;
    }
    //For the Managers who can set a ticket to be either approved or denied
    public Tickets(int aToSet, int rToSet, int idToSet, string dToSet, decimal money, Status sToSet){
        Author = aToSet;
        ID = idToSet;
        Description = dToSet;
        this.Amount = money;
        State = sToSet;
        Resolver=rToSet;
    }
    public override string ToString(){
        return $"ID: {this.Author} \nID of Ticket: {this.ID} \nAmount Requested: ${this.Amount}\nReason:{this.Description}";
    }
}
