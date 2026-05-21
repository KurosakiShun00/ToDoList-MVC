using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.Models;
using ToDoList_MVC.Services;

//da notare che non è più prevista la creazione di un todo che non appartenga ad una lista
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
                return BadRequest();
            }

            return Ok(createdToDo);
        }

        //##### TODO SINGOLI
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

        [HttpGet("todos/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _toDoService.GetByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        //se aggiungo l'id della lista posso controllare se appartiene a quella listae dare una bad request nel caso no
        [HttpPut("todos/{ToDoId}")]
        public async Task<IActionResult> UpdateToDo(int ToDoId, ToDoDTO newToDo)
        {
            var Updated = await _toDoService.UpdateAsync(ToDoId, newToDo);

            if (!Updated) return BadRequest();

            return Ok(Updated);

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