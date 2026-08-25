using Microsoft.EntityFrameworkCore;
using ToDoList_MVC.Data;
using ToDoList_MVC.Models;

namespace ToDoList_MVC.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ToDoDB _context;
    
    public  CategoryRepository(ToDoDB context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllCategories(string? userId)
    {
        return await _context.Categories.Where(x => x.UserId == userId).ToListAsync();
    }


    public async Task<int> CountCompleted(int id, string? userId)
    {
        return (from t in _context.ToDos
                join c in _context.Categories on t.CategoryId = ).CountAsync();
    }
    
    
    Task<int> CountNotCompleted(int id, string? userId);
    
}