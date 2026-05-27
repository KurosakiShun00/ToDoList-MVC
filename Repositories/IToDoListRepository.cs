using ToDoList_MVC.Models;

namespace ToDoList_MVC.Repositories
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<ToDoList>> GetAllAsync();
        Task<ToDoList?> GetByIdAsync(int id);
        Task<bool> ListExistsAsync(int id);
        Task AddListAsync(ToDoList newList);
        Task AddToDoAsync(ToDo newToDo);
        void DeleteList(ToDoList list);
        Task SaveChangesAsync();
    }
}
