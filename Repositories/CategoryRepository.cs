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
        return await (from t in _context.ToDos
                join c in _context.Categories on t.CategoryId equals c.Id
                join l in _context.ToDosLists on t.ToDoListId equals l.Id
                where t.IsCompleted == true && c.Id == id && l.UserId == userId
                select t).CountAsync();
    }
    
    
    public async Task<int> CountNotCompleted(int id, string? userId)
    {
        return await (from t in _context.ToDos
            join c in _context.Categories on t.CategoryId equals c.Id
            join l in _context.ToDosLists on t.ToDoListId equals l.Id
            where t.IsCompleted == false && c.Id == id && l.UserId == userId
            select t).CountAsync();
    }

    public async Task<Category?> CreateCategoryAsync(Category newCategory)
    {
      var entry = await _context.Categories.AddAsync(newCategory);
      
      await _context.SaveChangesAsync();
      
      return entry.Entity;
    }

    public async Task<Category?> GetCategoryById(int id, string? userId)
    {
        
        return await _context.Categories.Where(x => x.Id == id && x.UserId == userId).SingleOrDefaultAsync();
    }

    public async Task<Category?> UpdateCategoryAsync(int id, Category updatedCategory)
    {
        _context.Categories.Update(updatedCategory);
        await _context.SaveChangesAsync();
        return await _context.Categories.FirstOrDefaultAsync(l => l.Id == id);
        
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public void  DeleteCategory(Category category)
    {
        _context.Categories.Remove(category);
    }
}