using CustomExceptions;
using System.Text.Json;
using System.Text; 
using Models;


namespace ConsoleFrontEnd
{
    public class TicketMenu
    {
        /// <summary>
        /// This will talk with the website and will operate as a selection screen for the employee
        /// </summary>
        /// <param name="you">The currect User</param>
        /// <param name="api">The url for the website</param>
        /// <returns>The completed task</returns>
        public async Task ETicket(Users you,string api)
        {
            Console.WriteLine($"Welcome {you.role} # {you.userId}!\nWhat would you like to do today?\n1) View Tickets\n2) Create a Ticket");
            if ((int)new WarningFixer().Parsing() == 1)
            {
                Console.WriteLine("Would you like those organized in a particular fashion?\n1) Collected by Status\n2) View a single ticket\n3) No Particular collection");
                int sel = (int) new WarningFixer().Parsing();
                await new TicketSelection().EmployeeSelection(you, sel, api);
            }
            else
            { 
                try
                {
                    await new EmployeeMenu().SubmitReimbursement(you, api);
                }
                catch (InvalidCredentialsException)
                {
                    Console.WriteLine("You didn't enter in the right information.");
                }
                catch (UsernameNotAvailable)
                {
                    Console.WriteLine("Something weird just happened.");
                }
            }
        }
        /// <summary>
        /// This will talk with the website and will operate as a selection screen for the manager
        /// </summary>
        /// <param name="you">The current user</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        public async Task MTicket(Users you,string api)
        {
            Console.WriteLine($"Welcome {you.username}!\nWhat would you like to do today?\n1) View Tickets\n2) Update a ticket\n3) View Users\n4) Change a current users role\n5) Fire a user");
            int sim = (int)new WarningFixer().Parsing();
            if (sim== 1)
            {
                Console.WriteLine("Would you like those organized in a particular fashion?\n1) Collected by Status\n2) View a single ticket\n3) From a particular associate\n4) No particular order");
                int sel = (int)new WarningFixer().Parsing();
                await new TicketSelection().ManagerSelection(sel, api);
            }
            else if (sim== 2)
            { 
                try
                {
                    await new ManagerMenu().UpdateReimbursement(you,api);
                }
                catch (InvalidCredentialsException)
                {
                    Console.WriteLine("Somethign wasn't input properly");
                }
                catch (ResourceNotFoundException)
                {
                    Console.WriteLine("That ticket has already been updated");
                }
            }else if (sim== 4)
            { 
                try
                {
                    await new ManagerMenu().ChangeUser(api);
                }
                catch (UsernameNotAvailable)
                {
                    Console.WriteLine("Something wasn't input properly");
                }
                catch (ResourceNotFoundException)
                {
                    Console.WriteLine("That ticket has already been updated");
                }

            }else if (sim == 5)
            {
                Console.WriteLine("What is the user you wish to fire?");
                int userToFire = (int)new WarningFixer().Parsing();
                string serializedUser = JsonSerializer.Serialize(userToFire);
                StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                HttpResponseMessage response = await http.DeleteAsync(api + "fire/"+ userToFire);
                if ((int)response.StatusCode == 202)
                {

                    bool? user = JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync());
                    if (user != null)
                    {
                        Console.WriteLine($"User {userToFire} has been fired.");

                    }
                } 
                else
                {
                    Console.WriteLine($"User {userToFire} could not be fired. Check to see if that user exists in the system first.");
                }
            }
            else
            {
                Console.WriteLine("Which selection of users would you like to see:\n1) All Users\n2) Specific User By their username\n3) Specific User by their userID");
                int sel = (int)new WarningFixer().Parsing();
                switch (sel)
                {
                    case 2:
                        try
                        {
                            await new ManagerMenu().ViewUserByUsername(api);
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("No user has that username.");
                        }
                        break;
                    case 3: 
                        try
                        {
                            await new ManagerMenu().ViewUserByID(api);
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("No user has that user ID.");
                        }
                        break;
                    default: 
                        try
                        {
                            await new ManagerMenu().ViewUsers(api);
                        }
                        catch (ResourceNotFoundException)
                        {
                            Console.WriteLine("The database is empty.");
                        }
                        break;
                }
            }
        }
    }
   
}
