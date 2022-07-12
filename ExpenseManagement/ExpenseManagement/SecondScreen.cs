using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace UI
{
    public class SecondScreen
    {
        public int Employee(Users user)
        {
            Console.WriteLine($"Welcome {user.userId}!\nWhat would you like to do today?\n 1) View Tickets\n2) Create a Ticket");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("Would you like those organized in a particular fashion?\n1) Collected by Status\n2) View a single ticket\n3) No Particular collection");
                int sel = int.Parse(Console.ReadLine());
                switch (sel)
                {
                    case 1:
                        return 11;
                    case 2:
                        return 12;
                    case 3:
                        return 13;
                    default:
                        return 0;
                }                
            }
            else
            {
                return 2;
            }
        }
        public int Manager(Users user)
        {
            Console.WriteLine($"Welcome {user.username}!\nWhat would you like to do today?\n 1) View Tickets\n2) Update a ticket");
            if(int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("Would you like those organized in a particular fashion?\n1) Collected by Status\n2) View a single ticket\n3) From a particular associate\n4) No particular order");
                int sel = int.Parse(Console.ReadLine());
                switch (sel)
                {
                    case 1:
                        return 101;
                    case 2:
                        return 102;
                    case 3:
                        return 103;
                    case 4:
                        return 104;
                    default:
                        return 0;
                }
            }
            else
            {
                return 20;
            }
        }
    }
}
