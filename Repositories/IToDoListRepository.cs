using ToDoList_MVC.Models;

namespace ToDoList_MVC.Repositories
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<ToDoList>> GetAllAsync();
        Task<ToDoList?> GetByIdAsync(int id);
        Task<bool> ListExistsAsync(int id);
        Task AddListAsync(ToDoList newList);
        Task<ToDoList?> UpdateListAsync(int id, ToDoList updateList);
        void DeleteList(ToDoList list);
        Task AddToDoAsync(ToDo newToDo);
        Task UpdateToDoAsync(int id, ToDo newToDo);
        void DeleteToDoAsync(ToDo id);
        Task<ToDo?> GetToDoAsync(int id);
        Task SaveChangesAsync();
    }
}
