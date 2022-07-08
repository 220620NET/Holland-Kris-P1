using Models;
using DataAccess;

namespace Services;
public class TodoService
{
    private TodoDAO todoDao = new TodoDAOImpl();

    public List<Todo> GetAllTodos(){
        return todoDao.GetAllTodos();
    }
    public bool CreateTodo(Todo todo){
        if(todo.description.Length>10){
            return false;
        }
        return todoDao.CreateTodo(todo);
    }
    public void DeleteOneTodo(int todoId){
        todoDao.DeleteOneTodo(todoId);
    }
}
