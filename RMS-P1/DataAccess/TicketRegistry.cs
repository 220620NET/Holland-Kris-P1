using Models;
using System.Text.Json;

namespace DataAccess;

public class TicketRegistry{
    private const string file = "../DataAccess/ticketRegistry.json";
    
    /* Get Users
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

    public Tickets GetTicketByID(int ID){
        try{
            Dictionary<int,Tickets> ticket = GetTickets();
            return ticket[ID];
        }catch(JsonException){
            throw;
        }
    } 
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
