using ToDoList_MVC.Models;

namespace ToDoList_MVC.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategories(string? userId);
    Task<int> ToDoCompletedCount(int id, string? userId);
    Task<int> ToDoNotCompletedCount(int id, string? userId);
    
}