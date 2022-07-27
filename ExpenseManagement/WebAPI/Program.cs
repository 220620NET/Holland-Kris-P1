
using Services;
using CustomExceptions;
using DataAccess;
using Models;
using WebAPI.Controllers;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

/*  Scoped Services
 *  For every service I need a connection to the database hence the ConnectionFactory
 *  For all User endpoints I need UserServices and UserRepository to substitute IUserDAO
 *  For all Ticket endpoints I need TicketServices and TicketRepository to substitute ITicketDAO
 *  For all Authorization I need AuthServices
 *  Finally I will need the controllers for the API otherwise, I might as well not have a project
//  */
// builder.Services.AddDbContext<ExpenseDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("P1DB")));
builder.Services.AddSingleton(ctx => ConnectionFactory.GetInstance(builder.Configuration.GetConnectionString("P1DB")));
builder.Services.AddScoped<IUserDAO, UserRepository>();
builder.Services.AddScoped<ITicketDAO, TicketRepostitory>();
builder.Services.AddTransient<AuthServices>();
builder.Services.AddTransient<UserServices>();
builder.Services.AddTransient<TicketServices>();
builder.Services.AddScoped<AuthController>();
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<TicketController>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Welcome! Please type in /swagger/index.html at the end of the url above to access all the fun goodness.");


/*  Authorization Endpoints
 *  
 *  /register will take in a json body and convert the information into a User model that will then be sent to Services to be Registered
 *  /login will take in a json body and convert the information into a User model that will then be sent to Services to be Logged in
 */
app.MapPost("/register", (Users user, AuthController controller) =>controller.Register(user));
app.MapPost("/login", (Users user, AuthController controller) => controller.Login(user));
app.MapPut("/reset", (Users user, AuthController controller) => controller.Reset(user));
app.MapPut("/payroll", (Users user, AuthController controller) => controller.PayRollChange(user));
/*  User Endpoints
 *  
 *  /users can be entered in the url bar and will return a json reading of all users in the database. There is no hashing of the passwords as of now
 *  /users/id/{id} can be entered in the url bar and will return a json reading of the specfied user
 *  /users/name/{username} can be entered in the url bar and will return a json reading of the specfied user
 */
app.MapGet("/users", (UserController controller) =>controller.GetAllUsers());
app.MapGet("/users/id/{id}", (int id, UserController controller) => controller.GetUserByID(id));
app.MapGet("/users/name/{username}", (string username, UserController controller) => controller.GetUserByUsername(username));
app.MapDelete("/fire/{id}",(int id, UserController controller) => controller.DeleteUser(id));

/*  Ticket Endpoints
 *  
 *  /submit will take in a json body and convert the information into a Ticket model that will then be sent to Services to be Created
 *  /process will take in a json body and convert the information into a Ticket model that will then be sen to Services to be Updated
 *  /tickets/author/{authorID} can be entered in the url bar and will return a json reading of the list of tickets that that user has made or an error if that user hasn't made any or doesn't exist
 *  /tickets/id/{ticketNum} can be entered in the url bar and will return a json reading of the specified ticket or error if that ticket doesn't exist
 *  /tickets/status/{state} can be entered in the url bar and will return a json reading of the list of tickets that have the specified status
 */
app.MapPost("/submit", (Tickets newTicket, TicketController controller) => controller.Submit(newTicket));
app.MapPut("/process", (Tickets newTicket, TicketController controller) => controller.Process(newTicket));
app.MapGet("/tickets/author/{authorID}", (int authorID, TicketController controller) => controller.GetTicketByAuthor(authorID));
app.MapGet("/tickets/id/{ticketNum}", (int ticketNum, TicketController controller) => controller.GetTicketByTicketNum(ticketNum));
app.MapGet("/tickets/status/{state}", (string state, TicketController controller) => controller.GetTicketByStatus(state));
app.MapGet("/tickets", (TicketController controller) => controller.GetAllTickets());
app.Run();