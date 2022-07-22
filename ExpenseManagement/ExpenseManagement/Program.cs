using Models;
using ConsoleFrontEnd;

/* Console Front End
 * 
 *  This will be how the user directly interacts with the menus and tickets
 *      An employee will only see their information
 *      A manager can see everything
 *  
 *  The menu will loop as long as the user wants to view things
 */
Console.WriteLine("Welcome to Kris's Expense Management System program.");
Console.WriteLine("This serves as the Foundation project for Revature LLC.");
Console.WriteLine("The Console communicates with a built and published API on Microsoft's azure server");
Console.WriteLine("Enjoy the exprience!");
while (true)
{
    Users me = await new MainMenu().Start();
    Console.Clear();
    bool running;
    do
    {
        running = await new MainMenu().Selection(me);
        Console.Clear();
    } while (running);
    Console.WriteLine($"Goodbye {me.username}");
}