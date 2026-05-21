using ToDoList_MVC.Models;
using ToDoList_MVC.Repositories;

namespace ToDoList_MVC.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly ToDoListRepository _repository;

        public ToDoListService(ToDoListRepository repository) {
        _repository = repository;
        }

        public async Task<IEnumerable<ToDoList>> GetAllListsAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<ToDoList> CreateListAsync(ToDoList newList)
        {
            await _repository.AddListAsync(newList);
            await _repository.SaveChangesAsync();
            return newList;
        }
        public async Task<ToDo?> AddToDoToListAsync(int listId, ToDo newToDo)
        {
            var exists = await _repository.ListExistsAsync(listId);
            if (!exists) return null;

            newToDo.ToDoListId = listId;

            await _repository.AddToDoAsync(newToDo);
            await _repository.SaveChangesAsync();
            return newToDo;

        }
        public async Task<bool> DeleteListAsync(int id)
        {
            var list = await _repository.GetByIdAsync(id);
            if (list == null) return false;

            _repository.DeleteList(list);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
