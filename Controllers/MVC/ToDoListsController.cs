using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.Models;
using ToDoList_MVC.Services;
using ToDoList_MVC.ViewModels;
using ToDoList_MVC.ViewModels.ToDo;
using ToDoList_MVC.ViewModels.ToDoList;

namespace ToDoList_MVC.Controllers.MVC;

public class ToDoListsController : Controller
{
    private readonly IToDoListService _service;

        public ToDoListsController(IToDoListService service)
        {
            _service = service;
        }

        private string? GetUserID()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserID();
            if(userId == null) return Unauthorized();
            
            var items = (await _service.GetAllListsAsync(userId)).ToList();
            

            var viewModels = items.Select(x => new ToDoListsListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ToDos = x.ToDos
            }).ToList();

            return View(viewModels);
        }

        

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserID();
            if(userId == null) return Unauthorized();
            
            var item = await _service.GetByIdAsync(id, userId);
            if (item == null) return NotFound();
            var ToDos = new List<ToDoListLineViewModel>();
            foreach (var ToDo in item.ToDos)
            {
                ToDos.Add(new ToDoListLineViewModel
                {
                    Id = ToDo.Id,
                    Name = ToDo.Name,
                    IsCompleted = ToDo.IsCompleted
                });
            }

            var viewModel = new ToDoListsDetailsViewModel()
            {
               Id               = item.Id,
               Name           = item.Name,
               ToDos = ToDos
    };
            return View(viewModel);
        }


        [Authorize]
        public IActionResult Create() => View();

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoListsCreateViewModel viewModel)
        {
            var userId = GetUserID();
            if(userId == null) return Unauthorized();
            
            if (!ModelState.IsValid) return View(viewModel);

            var new_list = new ToDoListDTO(viewModel);

            await _service.CreateListAsync(new_list, userId);
            return RedirectToAction(nameof(Index));
        }
        
        [Authorize]
        public async Task<IActionResult> CreateToDo(int id)
        {
            var userId = GetUserID();
            if(userId  == null) return Unauthorized();
            
            var list = await _service.GetByIdAsync(id, userId);
            if(list == null) return NotFound();
            var listName = list.Name;

            var viewModel = new ToDoCreateViewModel()
            {
                ToDoListId = id,
                ListName = listName
            };
            
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateToDo(ToDoCreateViewModel viewModel)
        {
            var userId = GetUserID();
            if(userId  == null) return Unauthorized();
            
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(viewModel.ListName))
                {
                    var list = await _service.GetByIdAsync(viewModel.ToDoListId, userId);
                    viewModel.ListName = list?.Name;
                }
                return View(viewModel);
            }
            
            var new_toDo = new ToDoDTO(viewModel);

            await _service.AddToDoToListAsync(new_toDo.ToDoListId, new_toDo, userId);

                    
            return RedirectToAction(nameof(Details), new {id =  new_toDo.ToDoListId});
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {   
            var userId = GetUserID();
            if(userId  == null) return Unauthorized();
            var item = await _service.GetByIdAsync(id, userId);
            if (item == null) return NotFound();
            var viewModel = new ToDoListsEditViewModel
            {
                Id = item.Id,
                Name = item.Name
            };
            return View(viewModel);

        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDoListsEditViewModel viewModel)
        {
            var userId = GetUserID();
            if(userId  == null) return Unauthorized();
            
            if (!ModelState.IsValid) return View(viewModel);

            var new_list = new Models.ToDoListDTO(viewModel);

            await _service.UpdateListAsync(id, new_list, userId);
            return RedirectToAction(nameof(Details), new {id =  id});
        }

        [Authorize]
                public async Task<IActionResult> Delete(int id)
                {
                    var userId = GetUserID();
                    if(userId  == null) return Unauthorized();

                    var item = await _service.GetByIdAsync(id, userId);
                    if (item == null) return NotFound();
                    var ToDos = new List<ToDoListLineViewModel>();
                    foreach (var ToDo in item.ToDos)
                    {
                        ToDos.Add(new ToDoListLineViewModel
                        {
                            Id = ToDo.Id,
                            Name = ToDo.Name,
                            IsCompleted = ToDo.IsCompleted
                        });
                    }

                    var viewModel = new ToDoListsDetailsViewModel()
                    {
                        Id               = item.Id,
                        Name           = item.Name,
                        ToDos = ToDos
                    };
                    return View(viewModel);

                }
                
                [Authorize]
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    var userId = GetUserID();
                    if(userId  == null) return Unauthorized();

                    bool isDeleted = await _service.DeleteListAsync(id, userId);

                    if (!isDeleted)
                    {
                        return NotFound();
                    }

                    return RedirectToAction(nameof(Index));
                }
                
                [Authorize]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> ToggleToDo(int id, int toDoListId)
                {
                    var item = await _service.GetToDoAsync(id);
                    if (item == null) return NotFound();

                    item.IsCompleted = !item.IsCompleted;

                    await _service.UpdateToDoAsync(id, item);

                    return RedirectToAction(nameof(Details), new { id = toDoListId });
                }

                [Authorize]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> EditToDo(int id, ToDoEditViewModel viewModel)
                {
                    var existingToDo = await _service.GetToDoAsync(id);
                    if (existingToDo == null) return NotFound();

                    existingToDo.Name = viewModel.Name;

                    await _service.UpdateToDoAsync(id, existingToDo);

                    int redirectId = viewModel.ToDoListId != 0 ? viewModel.ToDoListId : existingToDo.ToDoListId;
                    return RedirectToAction(nameof(Details), new { id = redirectId });
                }
                
                [Authorize]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteToDo(int id)
                {

                    bool isDeleted = await _service.DeleteToDoAsync(id);

                    if (!isDeleted)
                    {
                        return NotFound();
                    }

                    string? referer = Request.Headers["Referer"].ToString();

                    if (!string.IsNullOrEmpty(referer))
                    {
                        return Redirect(referer);
                    }

                    
                    return RedirectToAction(nameof(Index));
                }
    }
