using ToDoList_MVC.Models;

namespace ToDoList_MVC.Services
{
    public interface IToDoService
    {

        Task<IEnumerable<ToDoDTO>> GetAllAsync();
        Task<IEnumerable<ToDoDTO>> GetCompleteAsync();
        Task<ToDoDTO?> GetByIdAsync(int id);
        Task<ToDoDTO> CreateAsync(ToDoDTO to_do_dto);
        Task<bool> UpdateAsync(int id, ToDoDTO to_do_dto);
        Task<bool> DeleteAsync(int id);

    }
}
