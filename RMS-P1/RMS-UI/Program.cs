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
using Models;
Console.WriteLine("Works");