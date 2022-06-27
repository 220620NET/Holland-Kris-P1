namespace Models;

// Need class for tickets
//     int ID
//     string Author
//     string resolver
//     string description
//     enum status
//     decimal amount
public enum Status{
    Pending,
    Approved,
    Denied
}

public class Tickets{
    //Creating getter setters
    public int ID{get;set;}
    public string Author{get;set;}
    public string Resolver{get;set;}
    public Status status{get;set;}
    public string description{get;set;}
    public decimal amount{get;set;}

    //creating two constructors for the tickets one for the employee one for the manager
    public Tickets(int id, string name, string description, decimal amount){
        this.status = Pending;
        this.ID =id;
        this.Author = name;
        this.description = description;
        this.amount = amount;
    }

    public Tickets(int id, string nameA,string nameR, Status state, string description, decimal amount){
        this.status = state;
        this.ID =id;
        this.Author = nameA;
        this.Resolver = nameR;
        this.description = description;
        this.amount = amount;
    }

}