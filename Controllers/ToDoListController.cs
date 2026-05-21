using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.Models;
using ToDoList_MVC.Services;

namespace ToDoList_MVC.Controllers
{
    public class ToDoListController
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

            // GET: api/lists
            [HttpGet]
            public async Task<ActionResult<IEnumerable<ToDoList>>> GetLists()
            {
                var lists = await _todoListService.GetAllListsAsync();
                return Ok(lists);
            }

            // POST: api/lists
            [HttpPost]
            public async Task<ActionResult<ToDoList>> CreateList(ToDoList newList)
            {
                var createdList = await _todoListService.CreateListAsync(newList);

                if (createdList == null)
                {
                    return BadRequest();
                }

                return Created();
            }

            // POST: api/lists/{listId}/todos
            [HttpPost("{listId}/todos")]
            public async Task<IActionResult> AddToDoToList(int listId, ToDo newToDo)
            {
                var createdToDo = await _todoListService.AddToDoToListAsync(listId, newToDo);

                if (createdToDo == null)
                {
                    return NotFound();
                }

                return Ok(createdToDo);
            }

            // DELETE: api/lists/{id}
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
}
