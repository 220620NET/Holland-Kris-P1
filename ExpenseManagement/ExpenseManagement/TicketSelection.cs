using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;

namespace ConsoleFrontEnd
{
    public class TicketSelection
    {
        public TicketSelection() { }
        /// <summary>
        /// This handles the selection of the tickets section of the second screen for the employee
        /// </summary>
        /// <param name="you">The current user</param>
        /// <param name="sel">The selected option</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        public async Task EmployeeSelection(Users you,int sel,string api)
        {
            switch (sel)
            {
                case 1:
                    try
                    {
                        await new EmployeeMenu().ViewByStatus(you, api);
                    }
                    catch (ResourceNotFoundException)
                    {
                        Console.WriteLine("There are no tickets with that state that you have authored.");
                    }
                    break;
                case 2:
                    try
                    {
                        await new EmployeeMenu().ViewByID(api);
                    }
                    catch (ResourceNotFoundException)
                    {
                        Console.WriteLine("There is no ticket with that ID.");
                    }
                    break;
                case 3:
                    try
                    {
                        await new EmployeeMenu().ViewAll(you, api);
                    }
                    catch (ResourceNotFoundException)
                    {
                        Console.WriteLine("You have not made any tickets.");
                    }
                    break;
                default:
                    Console.WriteLine("I didn't understand that input.");
                    break;
            }
        }
        /// <summary>
        /// This handles the selection of the tickets section of the second screen for the manager
        /// </summary> 
        /// <param name="sel">Selected option</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        public async Task ManagerSelection(int sel,string api)
        {
            switch (sel)
            {
                case 1:
                    try
                    {
                        await new ManagerMenu().ViewTicketsByStatus(api);
                    }
                    catch (ResourceNotFoundException)
                    {
                        Console.WriteLine("There are no tickets with that state that you have authored.");
                    }
                    break;
                case 2:
                    try
                    {
                        await new ManagerMenu().ViewTicketByID(api);
                    }
                    catch (ResourceNotFoundException)
                    {
                        Console.WriteLine("There is no ticket with that ID.");
                    }
                    break;
                case 3:
                    try
                    {
                        await new ManagerMenu().ViewTicketsByAuthor(api);
                    }
                    catch (ResourceNotFoundException)
                    {
                        Console.WriteLine("That associate has not made any tickets yet.");
                    }
                    break;
                case 4:
                    try
                    {
                        await new ManagerMenu().ViewAllTickets(api);
                    }
                    catch (ResourceNotFoundException)
                    {
                        Console.WriteLine("Something went wrong.");
                    }
                    break;
                default:
                    Console.WriteLine("I did not understand your request please enter a number from the list.");
                    break;
            }
        }
    }
}
