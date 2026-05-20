using ToDoList_MVC.Models;

namespace ToDoList_MVC.Repositories
{
    public interface IToDoRepositories
    {

        Task<IEnumerable<ToDo>> GetAllAsync();
        Task<IEnumerable<ToDo>> GetCompleteTodosAsync();
        Task<ToDo?> GetByIdAsync(int id);
        Task AddAsync(ToDo to_do);
        Task DeleteAsync(ToDo to_do);
        Task SaveChangesAsync();
    }
}
