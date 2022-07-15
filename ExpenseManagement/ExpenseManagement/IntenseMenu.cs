using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;
using DataAccess;

namespace UI
{
    public class IntenseMenu
    {
        private static TicketServices _ticketServices;
        public IntenseMenu()
        {
            _ticketServices = new TicketServices(new TicketRepostitory());
        }
        public void ETickets(int selection,Users employee) 
        {
            switch (selection)
            {
                case 11:
                    Console.WriteLine("Which status would you like to see?\n0) Pending\n1) Approved\n2) Denied");
                    int k = (int)Parsing();
                    List<Tickets> all = _ticketServices.GetReimbursementByStatus(k);
                    Unique(all,employee);
                    break;
                case 12:
                    Console.WriteLine("Which ticket do you want to look at? Please enter the ticket number.");
                     int l=(int)Parsing();
                    Console.WriteLine(_ticketServices.GetReimbursementByID(l));
                    break;
                case 13:
                    List<Tickets> these = _ticketServices.GetReimbursementByUserID(employee.userId);
                    foreach(Tickets ticket in these)
                    {
                        Console.WriteLine(ticket);
                    }                    
                    break;
                case 2:
                    Console.WriteLine("How much are you requesting?");
                    decimal f = Parsing();
                    Console.WriteLine($"Why are you requesting {f}?");
                    string? reason = Console.ReadLine();
                    if (reason != null)
                    {
                        _ticketServices.SubmitReimbursement(new Tickets(employee.userId,reason,f));
                    }
                    else
                    {
                        Console.WriteLine("Please enter a reason next time. I am exiting for now but you may return");
                    }
                    break;
                default:
                    Console.WriteLine("I did not understand your request.");
                    break;

            }
        }
        public void MTickets(int selection, Users manager)
        {
            switch (selection)
            {
                case 101:
                    break;
                case 102:
                    break;
                case 103:
                    break;
                case 104:
                    break;
                case 20:
                    break;
                default:
                    break;

            }
        }
        public decimal Parsing()
        {
            int k = 0;
            while (k == 0)
            {
                try
                {
                    return decimal.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("That wasn't a number");
                    k = 0;
                }
            }
            return 0;
        }
        public void Unique(List<Tickets> all,Users you)
        {
            foreach (Tickets tickets in all)
            {
                if (tickets.author == you.userId)
                {
                    Console.WriteLine(tickets);
                }
            }
        }
    }
   
}
