using ToDoList_MVC.Models;

namespace ToDoList_MVC.Services
{
    public interface IToDoServices
    {

        Task<IEnumerable<ToDo>> GetAllAsync();
        Task<IEnumerable<ToDo>> GetCompleteAsync();
        Task<ToDo?> GetByIdAsync(int id);
        Task<ToDo> CreateAsync(ToDoDTO to_do_dto);
        Task<bool> UpdateAsync(int id, ToDoDTO to_do_dto);
        Task<bool> DeleteAsync(int id);

    }
}
