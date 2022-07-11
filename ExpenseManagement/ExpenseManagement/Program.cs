// See https://aka.ms/new-console-template for more information
using DataAccess;
using Models;
using Services;
using CustomExceptions;
/*Console.WriteLine("Here Are The entries right now");
TicketDAO ticketDAO = new TicketRepostitory();
List<Tickets> t = ticketDAO.GetAllTickets();
foreach(Tickets ti in t)
{
    Console.WriteLine(ti);
}
Console.WriteLine("Which ticket would you like to see?");

int k =int.Parse(Console.ReadLine());
Tickets s = ticketDAO.GetTicketsById(k);
Console.WriteLine(s);
Console.WriteLine("Done");*/

try
{
    Users newUser = new Users("Peter", "asdoans", 0);
    Console.WriteLine(new UserRepository().CreateUser(newUser));
}
catch (Exception)
{
    Console.WriteLine("No user was foudn with that ID");
}
List<Users> users = new UserServices().GetAllUsers();
foreach(Users user in users)
{
    Console.WriteLine(user);
}

