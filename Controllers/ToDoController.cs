using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.Services;
using ToDoList_MVC.Models;
namespace ToDoList_MVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoServices _todoService;

        public ToDoController(IToDoServices todoService)
        {
            _todoService = todoService;
        }

        //GET todo <-- perorso
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoService.GetAllAsync();

            return Ok(todos);
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompleted()
        {
            var completedTodos = await _todoService.GetCompleteAsync();
            return Ok(completedTodos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _todoService.GetByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToDoDTO to_do_dto)
        {
            var created_to_do = await _todoService.CreateAsync(to_do_dto);

            return Created();

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, ToDoDTO to_do_dto)
        {
            var isUpdated = await _todoService.UpdateAsync(id, to_do_dto);

            if(!isUpdated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _todoService.DeleteAsync(id);

            if (!isDeleted) return NotFound();
            

            return Ok();
        }

    }
}
