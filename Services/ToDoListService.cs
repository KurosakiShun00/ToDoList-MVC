using ToDoList_MVC.Models;
using ToDoList_MVC.Repositories;

namespace ToDoList_MVC.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _repository;

        public ToDoListService(IToDoListRepository repository) {
        _repository = repository;
        }

        public async Task<IEnumerable<ToDoListDTO>> GetAllListsAsync()
        {
            var listFromDB = await _repository.GetAllAsync();

            return listFromDB.Select(list => new ToDoListDTO(list));
        }

        public async Task<ToDoListDTO?> GetByIdAsync(int id)
        {
            var list = await _repository.GetByIdAsync(id);
            if(list == null) return null;
            
            var result = new  ToDoListDTO(list);
            
            return result;
        }
        public async Task<ToDoList> CreateListAsync(ToDoListDTO newListDTO)
        {
            var newList = new ToDoList
            {
                Name = newListDTO.Name
            };
            
            await _repository.AddListAsync(newList);
            await _repository.SaveChangesAsync();
            return newList;
        }
        public async Task<ToDo?> AddToDoToListAsync(int listId, ToDoDTO newToDo)
        {
            var exists = await _repository.ListExistsAsync(listId);
            if (!exists) return null;

            var _ToDo = new ToDo
            {
                Name = newToDo.Name,
                IsCompleted = newToDo.IsCompleted,
                ToDoListId = listId
            };

            await _repository.AddToDoAsync(_ToDo);
            await _repository.SaveChangesAsync();
            return _ToDo;

        }

        public async Task<ToDoList?> UpdateListAsync(int id, ToDoListDTO updatedListDTO)
        {
            if (!(await _repository.ListExistsAsync(id))|| id != updatedListDTO.Id) return null;

            var updateList = new ToDoList
            {
                Id = updatedListDTO.Id,
                Name = updatedListDTO.Name
            };
            
            var result = await _repository.UpdateListAsync(id, updateList);
            await _repository.SaveChangesAsync();
            
            return result;
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
