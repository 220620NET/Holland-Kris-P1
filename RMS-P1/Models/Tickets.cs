using CustomExceptions;
namespace Models;

/*  Status Enum
    This enumeration creates the status of the tickets and sets the default state of the ticket as "Pending"
    This is set as an enumeration to ensure that the proper terminology is provided and set instead of potential user error
*/
public enum Status{
    Pending=0,
    Approved=1,Denied=2
}

/*  Tickets Class
    This is the class that will be used to create, modify and present ticket information
    This class holds the following information and allows the user to get or set them:
        The ID of the  Author of the Ticket as an integer 
        The ID of the Resolver of the Ticket as an integer
        The ID of the Ticket as an integer
        The Amount of money the Ticket is for as a decmial rounded to 2 digits
        The Description for the reason for the Ticket as a String
        The State of the Ticket with an automatic default to "Pending"
    
    There are two constructors
        One for the employee
        One for the manager

        This may be simplified to one later because I might just want the manager to simply edit an existing
        ticket rather than create a new ticket with the same ID.
*/
//Basic class for Tickets setting an Author and Resolver will require its own login
public class Tickets{
    public int Author{get;set;}
    public int ID{get;set;}
    public int Resolver{get;set;}
    public string Description{get;set;}
    public Status State{get;set;}
    private decimal Amount{get;set;}
    //For the Employee making the ticket
    public Tickets (){}
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
