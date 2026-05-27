using ToDoList_MVC.Models;

namespace ToDoList_MVC.Services
{
    public interface IToDoListService
    {
        Task<IEnumerable<ToDoListDTO>> GetAllListsAsync();
        Task<ToDoList> CreateListAsync(ToDoList newList);
        Task<ToDo?> AddToDoToListAsync(int listId, ToDoDTO newToDo);
        Task<bool> DeleteListAsync(int id);
    }
}
