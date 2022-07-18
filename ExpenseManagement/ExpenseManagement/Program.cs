// See https://aka.ms/new-console-template for more information
using Models;
using ConsoleFrontEnd;
//new MainMenu(new AuthServices(new UserRepository())).Start();
Users me = await new MainMenu().Start();
await new MainMenu().Selection(me);