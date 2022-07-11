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
Users newUser = new Users("Mark", "sakd", 1);
try
{
    bool user = new AuthServices().Register(newUser);
    Console.WriteLine(user);
} catch (UsernameNotAvailable e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine("incorrect username or password");
}

List<Users> users = new UserRepository().GetAllUsers();
foreach (Users user in users)
{
    Console.WriteLine(user);
}
