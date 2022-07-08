using Models;

public interface TodoDAO{
    public List<Todo> GetAllTodos();
    public bool CreateTodo(Todo todo);
    public void DeleteOneTodo(int id);
}