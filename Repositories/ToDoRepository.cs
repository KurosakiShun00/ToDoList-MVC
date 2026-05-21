using Microsoft.EntityFrameworkCore;
using ToDoList_MVC.Data;
using ToDoList_MVC.Models;

namespace ToDoList_MVC.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDB _context;

        public ToDoRepository(ToDoDB context){      
            
                _context = context;
            }

        public async Task<IEnumerable<ToDo>> GetAllAsync()
        {
            return await _context.toDos.ToListAsync();
        }

        public async Task<IEnumerable<ToDo>> GetCompleteTodosAsync()
        {
            return await _context.toDos.Where(t => t.IsCompleted).ToListAsync();
        }

        public async Task<ToDo?> GetByIdAsync(int id)
        {
            return await _context.toDos.FindAsync(id);
        }

        public async Task AddAsync(ToDo to_do)
        {
            await _context.toDos.AddAsync(to_do);
        }

        public async Task DeleteAsync(ToDo to_do)
        {
             _context.toDos.Remove(to_do);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
