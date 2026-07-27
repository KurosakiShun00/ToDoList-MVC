using Microsoft.EntityFrameworkCore;
using ToDoList_MVC.Data;
using ToDoList_MVC.Models;

namespace ToDoList_MVC.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoDB _context;

        public ToDoListRepository(ToDoDB context)
        {  _context = context; }

        public async Task<IEnumerable<ToDoList>> GetAllAsync()
        {
            return await _context.ToDosLists.Include(l => l.ToDos).ToListAsync();
        }
        public async Task<ToDoList?> GetByIdAsync(int id)
        {
            return await _context.ToDosLists.Include(l => l.ToDos).FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task<bool> ListExistsAsync(int id)
        {
            return await _context.ToDosLists.AnyAsync(l => l.Id == id); 
        }
        public async Task AddListAsync(ToDoList newList)
        {
            await _context.ToDosLists.AddAsync(newList);
        }

        public async Task<ToDoList?> UpdateListAsync(int id, ToDoList updateList)
        {
             _context.ToDosLists.Update( updateList);
             await _context.SaveChangesAsync();
            return await _context.ToDosLists.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddToDoAsync(ToDo newToDo)
        {
            await _context.ToDos.AddAsync(newToDo);
        }
        public void DeleteList(ToDoList list)
        {
            _context.ToDosLists.Remove(list);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
