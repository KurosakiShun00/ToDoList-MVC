using ToDoList_MVC.Models;
using ToDoList_MVC.Repositories;
using ToDoList_MVC.ViewModels.Category;

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

    public async Task<Category?> GetCategoryById(int id, string? userId)
    {
        return await _repo.GetCategoryById(id, userId);
    }

    public async Task<Category?> UpdateCategoryAsync(int id, Category updatedCategory, string? userId)
    {
        var oldCategory = await _repo.GetCategoryById(id, userId);
        if (oldCategory == null) return null;
        
        oldCategory.Name = updatedCategory.Name;
        oldCategory.Color = updatedCategory.Color;
        
        return await _repo.UpdateCategoryAsync(id, oldCategory);

    }

    public async Task<bool> DeleteCategoryAsync(int id, string? userId)
    {
        var category = await _repo.GetCategoryById(id,userId);
        if (category == null) return false;

        _repo.DeleteCategory(category);
        await _repo.SaveChangesAsync();
        return true;
    }
}