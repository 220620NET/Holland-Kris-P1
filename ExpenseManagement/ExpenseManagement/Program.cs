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
while (true)
{
    Users me = await new MainMenu().Start();
    bool running;
    do
    {
        running = await new MainMenu().Selection(me);
    } while (running);
}