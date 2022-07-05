using Models;
using System.Text.Json;

namespace DataAccess;
/*  TicketRegistry Class
    This class holds methods to read and search for a Ticket
    This class can also add Tickets and will print all Tickets to a json file when done.
*/
public class TicketRegistry{
    private const string file = "../DataAccess/ticketRegistry.json";
    
    /* GetTickets method
        Will attempt to read a Json file and provide all detatils
        If the Json file is empty it will return a new Dictionary
    */
    public Dictionary<int, Tickets> GetTickets(){
        string fs = File.ReadAllText(file);
        try{
            return JsonSerializer.Deserialize<Dictionary<int, Tickets>>(fs);
        }catch(JsonException){

            return new Dictionary<int, Tickets>();
        }
    }
    /* GetTicketsByID
        Will attempt to locate a ticket with a given ID by implementing the GetTickets() Method
            If no such ticket exists it will throw an exception to the user
            Otherwise it returns the ticket to the user
    */
    public Tickets GetTicketByID(int ID){
        try{
            Dictionary<int,Tickets> ticket = GetTickets();
            return ticket[ID];
        }catch(JsonException){
            throw;
        }
    } 
    /*  AddTicket
        This method will add a ticket to the dictionary of tickets and will inform the user if an error occurs
    */
    public Tickets AddTicket(Tickets newTicket){
        try{
            Dictionary<int,Tickets> allTickets=GetTickets();
            allTickets.Add(newTicket.ID, newTicket);
            File.WriteAllText(file, JsonSerializer.Serialize(allTickets));
            return newTicket;
        }catch(JsonException){
            throw;
        }
    }

}
