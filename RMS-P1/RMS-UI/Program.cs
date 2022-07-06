/*  Expense Reimbursement management
    This is a management system where a user can either be an employee or an employer
    An employee can:
        submit a ticket asking for reimbursement
        view status of ticket
            maybe gets a notification for a change in ticket with a ticket number
        
    
    A manager can:
        view status of all tickets
        change status of pending ticket
            accepted
            denied
        cannot change any other ticket or any information other than state of a ticket
    Maybe the ticket comes from a separte file so need to use System.file
    Need a userId  for employees and managers
    Need classes for a Users
        string userID
        string name
        enum Role
        string password
    
    Need class for tickets
        int ID
        string Author
        string resolver
        string description
        string status
        decimal amount
    
    Need three custom exceptions
        ResourceNotFound
        UsernameNotAvailable
        InvalidCreditials
*/
using DataAccess;
using Models;
using System.Text.Json;
/* Creating a Test User and Ticket for Ticket Registry and User Registry
    This will be removed before the end of the week but this is an intermediate step to ensure that the Program will function properly
    
    As of 7/5/2022 This program functions and can do the following
        It can add a user to a json file
        It can add a ticket to a json file
        It can request a Specific user or ticket by a provided ID for Either the author of the ticket or the User ID
*/
Console.WriteLine("Welcome: \nWhat is your Personal ID?");
int iD = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("How much is your request for?");
decimal k = Convert.ToDecimal(Console.ReadLine());
decimal mon = Decimal.Round(k,2);
Console.WriteLine($"Why are you requesting {mon}?");
string d = Console.ReadLine();
Random numTicket = new Random();

Tickets Kris=new Tickets(iD,numTicket.Next(300),d, mon);
Users Sam= new Users(iD, "Sam","Password", Roles.Manager);
Console.WriteLine("Do I have your information right?");
Console.WriteLine(Kris.ToString());
/* Added a Try catch block chain so that I can add more than one and test if duplicates can happen
    They cannot. Happy days
*/
try{
    new UserRegistry().AddUser(Sam);
}catch(ArgumentException ex){
    Console.WriteLine("No Can do.");
}try{
    new TicketRegistry().AddTicket(Kris);
} catch(ArgumentException ex){
    Console.WriteLine(ex);
}
