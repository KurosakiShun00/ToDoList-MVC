using ToDoList_MVC.Models;

namespace ToDoList_MVC.Services
{
    public interface IToDoListService
    {
        Task<IEnumerable<ToDoList>> GetAllListsAsync();
        Task<ToDoList> CreateListAsync(ToDoList newList);
        Task<ToDo?> AddToDoToListAsync(int listId, ToDo newToDo);
        Task<bool> DeleteListAsync(int id);
    }
}
