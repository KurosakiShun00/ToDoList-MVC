using ToDoList_MVC.Models;
using ToDoList_MVC.ViewModels.Category;

namespace ToDoList_MVC.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategories(string? userId);
    Task<Category?> GetCategoryById(int id);
    Task<int> ToDoCompletedCount(int id, string? userId);
    Task<int> ToDoNotCompletedCount(int id, string? userId);
    Task<Category?> CreateCategoryAsync(Category newCategory);
    Task<Category?> UpdateCategoryAsync(int id, Category updatedCategory);
}