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
        public async Task<ActionResult<ToDoList>> CreateList(ToDoList newList)
        {
            var createdList = await _todoListService.CreateListAsync(newList);

            if (createdList == null)
            {
                return BadRequest();
            }


            return Ok(createdList);
        }


        [HttpPost("{listId}/todos")]
        public async Task<IActionResult> AddToDoToList(int listId, ToDoDTO newToDo)
        {
            var createdToDo = await _todoListService.AddToDoToListAsync(listId, newToDo);

            if (createdToDo == null)
            {
                return NotFound();
            }

            return Ok(createdToDo);
        }

        //#####
        [HttpPut("~/api/todos/{ToDoId}")]
        public async Task<IActionResult> UpdateToDo(int ToDoId, ToDoDTO newToDo)
        {
            var isUpdated = await _toDoService.UpdateAsync(ToDoId, newToDo);

            if (!isUpdated) return NotFound();

            return NoContent();

        }

        [HttpDelete("~/api/todos/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _toDoService.DeleteAsync(id);

            if (!isDeleted) return NotFound();


            return Ok();
        }
        //#####

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var deleted = await _todoListService.DeleteListAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}