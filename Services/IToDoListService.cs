using ToDoList_MVC.Models;

namespace ToDoList_MVC.Services
{
    public interface IToDoListService
    {
        Task<IEnumerable<ToDoListDTO>> GetAllListsAsync();
        Task<ToDoListDTO?> GetByIdAsync(int id);
        Task<ToDoList> CreateListAsync(ToDoListDTO newList);
        Task<ToDoList?> UpdateListAsync(int id, ToDoListDTO updatedListDTO);
        Task<ToDo?> AddToDoToListAsync(int listId, ToDoDTO newToDo);
        Task<bool> DeleteListAsync(int id);
    }
}
