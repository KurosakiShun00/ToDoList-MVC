using ToDoList_MVC.Models;
using ToDoList_MVC.Repositories;

namespace ToDoList_MVC.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Category>> GetAllCategories(string? userId)
    {
        var items = await _repo.GetAllCategories(userId);

        return items;

    }

    public async Task<int> ToDoCompletedCount(int id, string? userId)
    {
        var result = await _repo.CountCompleted(id, userId);
        return result;
        
    }

    public async Task<int> ToDoNotCompletedCount(int id, string? userId)
    {
        var result = await _repo.CountNotCompleted(id, userId);
        return result;
    }

    public async Task<Category?> CreateCategoryAsync(Category newCategory)
    {
        return await _repo.CreateCategoryAsync(newCategory);
    }
}