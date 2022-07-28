using System.ComponentModel.DataAnnotations;

namespace Models
{
    public enum Status
    {
        Pending,
        Approved,Denied
    }
    public class Tickets
    {
        [Key]
        public int ticketNum { get; set; }
        public Status status { get; set; }
        public int author { get; set; }
        public int resolver { get; set; }
        public string? description { get; set; }
        public decimal amount { get; set; }
        
        public Tickets()
        { 
            ticketNum = 0;
            status = Status.Pending;
            author = 0;
            resolver = 0;
            description = "";
            amount = 0;
        } 
        /// <summary>
        /// Constructor for creating a ticket
        /// </summary>
        /// <param name="aToSet">A valid user who authored this ticket</param>
        /// <param name="dToSet">A reason for the ticket</param>
        /// <param name="amToSet">The amount of money this ticket is worth</param>
        public Tickets(int aToSet, string? dToSet, decimal amToSet):this()
        {
            this.author = aToSet;
            this.description = dToSet;
            this.amount=amToSet;
        }
        
        public Tickets(Status state, int resolver):this()
        {
            this.status=state;
            this.resolver=resolver;
        }
        /// <summary>
        /// Constructor for updating and pulling tickets
        /// </summary>
        /// <param name="ticketNum">A unique identifier for the ticket from the Database</param>
        /// <param name="status">The status of the ticket saved as an enumerator{Pending = 0, Approved =1, Denied = 2}</param>
        /// <param name="author">A valid user who authored this ticket</param>
        /// <param name="resolver">A valid user who resolved this ticket</param>
        /// <param name="description">A reason for the ticket</param>
        /// <param name="amount">The amount of money this ticket is worth</param>
        public Tickets(int ticketNum, Status status, int author, int resolver, string? description, decimal amount):this()
        {
            this.ticketNum = ticketNum;
            this.status = status;
            this.author = author;
            this.resolver = resolver;
            this.description = description;
            this.amount = amount;
        }
        /// <summary>
        /// Method of transfering the string from the database representing the status of the ticket into an int
        /// </summary>
        /// <param name="s">String representing the status of the ticket</param>
        /// <returns>Integer representing the status of the ticket {Pending=0, Approved=1, Denied =2}</returns>
        public int StateToNum(string s)
        {
            switch (s)
            {
                case "Approved":
                    return 1;
                case "Denied":
                    return 2;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// Method of transfering the int corresponding to the status of the ticket into a string for the database representing the status of the ticket
        /// </summary>
        /// <param name="k">{Pending = 0, Approved =1, Denied =2}</param>
        /// <returns>String representing the status of the ticket</returns>
        public string NumToState(int k)
        {
            switch (k)
            {
                case 1:
                    return "Approved";
                case 2:
                    return "Denied";
                default:
                    return "Pending";
            }
        }
        
        public override string ToString()
        {
            return $"Ticket Number: {this.ticketNum}\nStatus: {this.status}\nAuthor: {this.author}\nResolver: {this.resolver}\nDescription: {this.description}\nAmount: {this.amount}";
        }
    }
}