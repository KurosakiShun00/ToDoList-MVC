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
            return await _context.toDosLists.Include(l => l.ToDos).ToListAsync();
        }
        public async Task<ToDoList?> GetByIdAsync(int id)
        {
            return await _context.toDosLists.FindAsync(id);
        }
        public async Task<bool> ListExistsAsync(int id)
        {
            return await _context.toDosLists.AnyAsync(l => l.Id == id); 
        }
        public async Task AddListAsync(ToDoList newList)
        {
            await _context.toDosLists.AddAsync(newList);
        }
        public async Task AddToDoAsync(ToDo newToDo)
        {
            await _context.toDos.AddAsync(newToDo);
        }
        public void DeleteList(ToDoList list)
        {
            _context.toDosLists.Remove(list);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
