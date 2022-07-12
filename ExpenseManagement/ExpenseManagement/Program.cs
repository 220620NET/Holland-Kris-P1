// See https://aka.ms/new-console-template for more information
using UI;
using Models;


Users user = new MainMenu().Start();
int selection = new MainMenu().Selection(user);
Console.WriteLine(selection);