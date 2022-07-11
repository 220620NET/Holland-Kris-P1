// See https://aka.ms/new-console-template for more information
using DataAccess;
using Models;
using Services;
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
Users user = new AuthServices().Login("Kris", "password");
Console.WriteLine(user);