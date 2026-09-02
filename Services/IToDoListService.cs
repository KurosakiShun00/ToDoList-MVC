using ToDoList_MVC.Models;

namespace ToDoList_MVC.Services
{
    public interface IToDoListService
    {
        Task<IEnumerable<ToDoListDTO>> GetAllListsAsync(string? userId);
        Task<ToDoListDTO?> GetByIdAsync(int id, string? userId);
        Task<ToDoList> CreateListAsync(ToDoListDTO newList, string? userId);
        Task<ToDoList?> UpdateListAsync(int id, ToDoListDTO updatedListDTO, string? userId);
        Task<ToDoDTO?> UpdateToDoAsync(int id, ToDoDTO updateToDoDTO);
        Task<bool> DeleteToDoAsync(int id, string? userId);
        Task<List<ToDoDTO>?> GetToDosFromListAsync(int listId, string? userId);
        Task<ToDoDTO?> GetToDoAsync(int id);
        Task<ToDo?> AddToDoToListAsync(int listId, ToDoDTO newToDo, string?  userId);
        Task<bool> DeleteListAsync(int id, string? userId);
        Task<List<ToDoDTO>?> GetAllToDos(string? userId);
    }
}
