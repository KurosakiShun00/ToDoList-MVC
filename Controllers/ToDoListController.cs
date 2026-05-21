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

        public ToDoListsController(IToDoListService todoListService)
        {
            _todoListService = todoListService;
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