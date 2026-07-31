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

        public async Task<IEnumerable<ToDoListDTO>> GetAllListsAsync(string? userId)
        {
            var listFromDB = await _repository.GetAllAsync(userId);

            return listFromDB.Select(list => new ToDoListDTO(list));
        }

        public async Task<ToDoListDTO?> GetByIdAsync(int id, string? userId)
        {
            var list = await _repository.GetByIdAsync(id, userId);
            if(list == null) return null;
            
            var result = new  ToDoListDTO(list);
            
            return result;
        }
        public async Task<ToDoList> CreateListAsync(ToDoListDTO newListDTO, string? userId)
        {
            var newList = new ToDoList
            {
                Name = newListDTO.Name,
                UserId = userId
            };
            
            await _repository.AddListAsync(newList);
            await _repository.SaveChangesAsync();
            return newList;
        }
        public async Task<ToDo?> AddToDoToListAsync(int listId, ToDoDTO newToDo, string? userId)
        {
            var exists = await _repository.ListExistsAsync(listId, userId);
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

        public async Task<ToDoDTO?> UpdateToDoAsync(int id, ToDoDTO updateToDoDTO)
        {
          
            var existingToDo = await _repository.GetToDoAsync(id);
            if (existingToDo == null) return null;

           
            existingToDo.Name = updateToDoDTO.Name;
            existingToDo.IsCompleted = updateToDoDTO.IsCompleted;

           
            await _repository.UpdateToDoAsync(id, existingToDo);

            return new ToDoDTO(existingToDo);
        }

        public async Task<bool> DeleteToDoAsync(int id)
        {
            var toDo = await _repository.GetToDoAsync(id);
            if (toDo == null) return false;

            _repository.DeleteToDoAsync(toDo);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<ToDoList?> UpdateListAsync(int id, ToDoListDTO updatedListDTO, string? userId)
        {
            if (!(await _repository.ListExistsAsync(id, userId))|| id != updatedListDTO.Id) return null;
            
            var updateList = new ToDoList
            {
                Id = updatedListDTO.Id,
                Name = updatedListDTO.Name
            };
            
            var result = await _repository.UpdateListAsync(id, updateList);
            await _repository.SaveChangesAsync();
            
            return result;
        }

        public async Task<ToDoDTO?> GetToDoAsync(int id)
        {
            var toDo = await _repository.GetToDoAsync(id);
            if(toDo == null) return null;
            ToDoDTO result = new ToDoDTO(toDo); 
            
            return result;
        }
        public async Task<bool> DeleteListAsync(int id, string? userId)
        {
            var list = await _repository.GetByIdAsync(id, userId);
            if (list == null) return false;

            _repository.DeleteList(list);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
