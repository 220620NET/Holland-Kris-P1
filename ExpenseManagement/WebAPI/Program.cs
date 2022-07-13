
using Services;
using CustomExceptions;
using DataAccess;
using Models;

var builder = WebApplication.CreateBuilder(args);

//dependency injection handled by ASP.NET Core
//Different ways to add your dependencies: Singleton, Scoped, Transient
//Singleton instances are shared throughtout the entire lifetime of the application
//Scoped instances are shared during the req/res lifecycle
//Transient instances are generated everytime it needs an instance of it
builder.Services.AddSingleton<ConnectionFactory>(ctx => ConnectionFactory.GetInstance(builder.Configuration.GetConnectionString("P1DB")));
builder.Services.AddScoped<IUserDAO, UserRepository>();
builder.Services.AddTransient<AuthServices>();
builder.Services.AddTransient<UserServices>();

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

/// <summary>
/// Returns all pokemon trainers in the db
/// </summary>
app.MapGet("/trainers", () =>
{
    //using ASP.NET core dependency injector
    var scope = app.Services.CreateScope();
    UserServices service = scope.ServiceProvider.GetRequiredService<UserServices>();

    return service.GetAllUsers();
});

//When we ask for a reference type such as PokeTrainer as a payload
//the framework will expect to receive this in the request body
//The ASP.NET Core will take the json data and turn it into PokeTrainer
//This is called "Model binding"
 

app.Run();