using ToDoList_MVC.Models;
using ToDoList_MVC.Repositories;

namespace ToDoList_MVC.Services;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _repository;

    public ToDoService(IToDoRepository repo)
    {
        _repository = repo;
    }

    public async Task<IEnumerable<ToDoDTO>> GetAllAsync()
    {
        var task = await _repository.GetAllAsync();

        return task.Select(t => new ToDoDTO(t)).ToList();
    }

    public async Task<IEnumerable<ToDoDTO>> GetCompleteAsync()
    {
        var task = await _repository.GetCompleteTodosAsync();

        return task.Select(t => new ToDoDTO(t)).ToList();
    }

    public async Task<ToDoDTO?> GetByIdAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null) return null;

        return new ToDoDTO(task);
    }

    public async Task<ToDoDTO> CreateAsync(ToDoDTO to_do_dto)
    {
        ToDo task = new()
        {
            Name = to_do_dto.Name,
            IsCompleted = to_do_dto.IsCompleted
        };

        await _repository.AddAsync(task);
        await _repository.SaveChangesAsync();

        return new ToDoDTO(task);
    }

    public async Task<ToDoDTO?> UpdateAsync(int listId, int id, ToDoDTO to_do_dto)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null || task.ToDoListId != listId) return null;

        task.Name = to_do_dto.Name;
        task.IsCompleted = to_do_dto.IsCompleted;

        await _repository.SaveChangesAsync();

        var task_2 = new ToDoDTO(task);
        return task_2;
    }

    public async Task<bool> PatchAsync(int listId, int id, ToDoPatch to_do_patch)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null || task.ToDoListId != listId) return false;

        if (to_do_patch.Name != null) task.Name = to_do_patch.Name;

        if (to_do_patch.IsCompleted.HasValue) task.IsCompleted = to_do_patch.IsCompleted.Value;

        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int listId, int id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null || task.ToDoListId != listId) return false;

        await _repository.DeleteAsync(task);
        await _repository.SaveChangesAsync();
        return true;
    }
}