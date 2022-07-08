using Models;
using DataAccess;
using Services;

TodoService todoService=new TodoService();

/*
    1) prints all todos
    2) create a todo
    3) delete todo
    0) EXIT
*/

bool running = true;
do
{
    Console.WriteLine("Todo APP\n1) Print all Todos\n2) Create A Todo\n3) Delete a Todo\n0) Exit");
    string? input = Console.ReadLine();
    switch (input){
        case "1":
            //print all todos
            PrintAllTodos();
            break;
        case "2":
            //create todo
            CreateTodos();
            break;
        case "3":
            //Deleting a todo
            DeleteRecord();
            break;
        case "0":
            //exit
            Console.Clear();
            Console.WriteLine("Goodbye!");
            running=false;
            break;
        default:
            Console.WriteLine("I didn't understand you input. Please Try again");
            break;
    }
} while (running);

void PrintAllTodos(){
    List<Todo> todo = todoService.GetAllTodos();
    foreach(Todo t in todo){
        Console.WriteLine(t);
    }
}
void CreateTodos(){
    Console.WriteLine("What task do you want to complete?");
    string? desc =  Console.ReadLine();
    Todo newT = new Todo(desc);
    if(todoService.CreateTodo(newT)){
        Console.WriteLine("Created!");
    }else{
        Console.WriteLine("Failed to create!");
    }
}
void DeleteRecord(){
    Console.WriteLine("Which index would you like to delete?");
    int ins = int.Parse(Console.ReadLine());
    todoService.DeleteOneTodo(ins);
}
