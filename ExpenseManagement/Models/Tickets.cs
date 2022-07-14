namespace Models
{
    public enum Status
    {
        Pending,
        Approved,Denied
    }
    public class Tickets
    {
        public int ticketNum { get; set; }
        public Status status { get; set; }
        public int author { get; set; }
        public int resolver { get; set; }
        public string? description { get; set; }
        public decimal amount { get; set; }
        public Tickets() { }
        //Will be used when creating a ticket
        public Tickets(int aToSet, string dToSet, decimal amToSet)
        {
            this.author = aToSet;
            this.description = dToSet;
            this.amount=amToSet;
        }
        //Will be used when adding from database
        public Tickets(int ticketNum, Status status, int author, int resolver, string description, decimal amount)
        {
            this.ticketNum = ticketNum;
            this.status = status;
            this.author = author;
            this.resolver = resolver;
            this.description = description;
            this.amount = amount;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
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
            return $"Ticket Number: {this.ticketNum}\nStatus: {this.status}\nAuthor: {this.author}\nResolver: {this.resolver}\nDescription: {this.description}\n Amount: {this.amount}";
        }
    }
}