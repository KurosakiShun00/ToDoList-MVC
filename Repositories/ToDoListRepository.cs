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

        public async Task<IEnumerable<ToDoList>> GetAllAsync(string? userId)
        {
            return await _context.ToDosLists.Include(l => l.ToDos).Where(u=>u.UserId == userId).ToListAsync();
        }
        public async Task<ToDoList?> GetByIdAsync(int id, string? userId)
        {
            return await _context.ToDosLists.Include(l => l.ToDos).ThenInclude(t=>t.Category).FirstOrDefaultAsync(l => l.Id == id && l.UserId==userId);
        }

        public async Task<List<ToDo>?> GetToDosFromListAsync(int listId)
        {
            return await _context.ToDos.Include(t=>t.Category).Where(l => l.ToDoListId == listId).ToListAsync();
        }

        public async Task<bool> ListExistsAsync(int id, string? userId)
        {
            return await _context.ToDosLists.AnyAsync(l => l.Id == id && l.UserId == userId); 
        }
        public async Task AddListAsync(ToDoList newList)
        {
            await _context.ToDosLists.AddAsync(newList);
        }

        public async Task<ToDoList?> UpdateListAsync(int id, ToDoList updateList)
        {
             _context.ToDosLists.Update(updateList);
             await _context.SaveChangesAsync();
            return await _context.ToDosLists.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<ToDo?> GetToDoAsync(int id)
        {
            return await _context.ToDos.FindAsync(id);
        }
        public async Task AddToDoAsync(ToDo newToDo)
        {
            await _context.ToDos.AddAsync(newToDo);
        }

        public async Task UpdateToDoAsync(int id, ToDo newToDo)
        {
             await _context.SaveChangesAsync();
        }
        
        public void DeleteToDoAsync(ToDo toDo)
        {
            _context.ToDos.Remove(toDo);
        }
        
        public void DeleteList(ToDoList list)
        {
            _context.ToDosLists.Remove(list);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<ToDo>?> GetAllToDos(string? userId)
        {
            return await _context.ToDosLists
                .Where(l => l.UserId == userId)
                .SelectMany(l => l.ToDos)
                .ToListAsync();
        }

    }
}
