using ToDoList_MVC.Models;

namespace ToDoList_MVC.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories(string? userId);
    Task<int> CountCompleted(int id, string? userId);
    Task<int> CountNotCompleted(int id, string? userId);
    Task<Category?> CreateCategoryAsync(Category newCategory);
}