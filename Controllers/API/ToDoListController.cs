using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.Models;
using ToDoList_MVC.Services;

namespace ToDoList_MVC.Controllers
{
    [ApiController]
    [Route("api/lists")]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoListService _todoListService;
        private readonly IToDoService _toDoService;

        public ToDoListsController(IToDoListService todoListService, IToDoService toDoService)
        {
            _todoListService = todoListService;
            _toDoService = toDoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoListDTO>>> GetLists()
        {
            var lists = await _todoListService.GetAllListsAsync();
            return Ok(lists);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoList>> CreateTodoListDto(ToDoList newList)
        {
            var createdList = await _todoListService.CreateListAsync(newList);
            if (createdList == null) return BadRequest();

            return Ok(createdList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var deleted = await _todoListService.DeleteListAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        [HttpGet("todos/all")]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _toDoService.GetAllAsync();
            return Ok(todos);
        }

        [HttpGet("todos/completed")]
        public async Task<IActionResult> GetCompleted()
        {
            var completedTodos = await _toDoService.GetCompleteAsync();
            return Ok(completedTodos);
        }

        [HttpPost("{listId}/todos")]
        public async Task<IActionResult> AddToDoToList(int listId, ToDoDTO newToDo)
        {
            var createdToDo = await _todoListService.AddToDoToListAsync(listId, newToDo);
            if (createdToDo == null) return BadRequest();

            return Ok(createdToDo);
        }

        [HttpGet("{listId}/todos/{id}")]
        public async Task<IActionResult> GetById(int listId, int id)
        {
            var todo = await _toDoService.GetByIdAsync(id);
            if (todo == null) return NotFound();

            return Ok(todo);
        }

        [HttpPut("{listId}/todos/{toDoId}")]
        public async Task<IActionResult> UpdateToDo(int listId, int toDoId, ToDoDTO newToDo)
        {
            var updated = await _toDoService.UpdateAsync(listId,toDoId, newToDo);
            if (updated == null) return BadRequest();

            return Ok(updated);
        }

        [HttpPatch("{listId}/todos/{toDoId}")]
        public async Task<IActionResult> PatchToDo(int listId, int toDoId, ToDoPatch toDoPatch)
        {
            var isPatched = await _toDoService.PatchAsync(listId, toDoId, toDoPatch);
            if(!isPatched) return BadRequest();

            return NoContent();
        }

        [HttpDelete("{listId}/todos/{id}")]
        public async Task<IActionResult> Delete(int listId, int id)
        {
            var isDeleted = await _toDoService.DeleteAsync(listId,id);
            if (!isDeleted) return BadRequest();

            return Ok();
        }
    }
}