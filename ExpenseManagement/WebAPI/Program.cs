
using Services;
using CustomExceptions;
using DataAccess;
using Models;
using WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

//dependency injection handled by ASP.NET Core
//Different ways to add your dependencies: Singleton, Scoped, Transient
//Singleton instances are shared throughtout the entire lifetime of the application
//Scoped instances are shared during the req/res lifecycle
//Transient instances are generated everytime it needs an instance of it
builder.Services.AddSingleton<ConnectionFactory>(ctx => ConnectionFactory.GetInstance(builder.Configuration.GetConnectionString("P1DB")));
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

app.MapGet("/", () => "Hi Auryn!");

//the curly braces are the route parameters 
app.MapGet("/greet/{name}", (string name) => {
    return $"Hi {name}!";
});

//Query parameter: these are key value pairs you pass in with your url after the question mark(?)
app.MapGet("/greet", (string name, string location) => {
    if (String.IsNullOrWhiteSpace(location))
        return $"Hello {name}";
    return $"Hello {name} from {location}";
});

app.MapPost("/register", (Users user, AuthController controller) =>controller.Register(user));
app.MapPost("/login", (Users user, AuthController controller) => controller.Login(user));

/// <summary>
/// Returns all users in the db
/// </summary>
app.MapGet("/users", (UserController controller) =>controller.GetAllUsers());
app.MapGet("/users/id/{id}", (int id, UserController controller) => controller.GetUserByID(id));
app.MapGet("/users/name/{username}", (string username, UserController controller) => controller.GetUserByUsername(username));

app.MapPost("/submit", (Tickets newTicket, TicketController controller) => controller.Submit(newTicket));
app.MapPost("/process", (Tickets newTicket, TicketController controller) => controller.Submit(newTicket));

app.Run();