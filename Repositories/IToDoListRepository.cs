using ToDoList_MVC.Models;

namespace ToDoList_MVC.Repositories;

public interface IToDoListRepository
{
    Task<IEnumerable<ToDoList>> GetAllAsync(string? userId);
    Task<ToDoList?> GetByIdAsync(int id, string? userId);
    Task<bool> ListExistsAsync(int id, string? userId);
    Task AddListAsync(ToDoList newList);
    Task<ToDoList?> UpdateListAsync(int id, ToDoList updateList);
    void DeleteList(ToDoList list);
    Task<List<ToDo>?> GetToDosFromListAsync(int listId);
    Task AddToDoAsync(ToDo newToDo);
    Task UpdateToDoAsync(int id, ToDo newToDo);
    void DeleteToDoAsync(ToDo id);
    Task<ToDo?> GetToDoAsync(int id);
    Task SaveChangesAsync();
    Task<List<ToDo>?> GetAllToDos(string? userId);
}