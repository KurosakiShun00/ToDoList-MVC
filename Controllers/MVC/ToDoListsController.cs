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

        
        public async Task<IActionResult> Index()
        {

            var items = (await _service.GetAllListsAsync()).ToList();
            

            var viewModels = items.Select(x => new ToDoListsListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ToDos = x.ToDos
            }).ToList();

            return View(viewModels);
        }



        
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
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


        // GET: /Customers/Create
        public IActionResult Create() => View();

        // POST: /Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoListsCreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var new_list = new ToDoListDTO(viewModel);

            await _service.CreateListAsync(new_list);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateToDo(int id)
        {
            var list = await _service.GetByIdAsync(id);
            if(list == null) return NotFound();
            var listName = list.Name;

            var viewModel = new ToDoCreateViewModel()
            {
                ToDoListId = id,
                ListName = listName
            };
            
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateToDo(ToDoCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(viewModel.ListName))
                {
                    var list = await _service.GetByIdAsync(viewModel.ToDoListId);
                    viewModel.ListName = list?.Name;
                }
                return View(viewModel);
            }

            var new_toDo = new ToDoDTO(viewModel);

            await _service.AddToDoToListAsync(new_toDo.ToDoListId, new_toDo);

                    
            return RedirectToAction(nameof(Details), new {id =  new_toDo.ToDoListId});
        }

        // GET: /Customers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            var viewModel = new ToDoListsEditViewModel
            {
                Id = item.Id,
                Name = item.Name
            };
            return View(viewModel);

        }
        
        // POST: /Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDoListsEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var new_list = new Models.ToDoListDTO(viewModel);

            await _service.UpdateListAsync(id, new_list);
            return RedirectToAction(nameof(Details), new {id =  id});
        }

        
                public async Task<IActionResult> Delete(int id)
                {

                    var item = await _service.GetByIdAsync(id);
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
                
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {

                    bool isDeleted = await _service.DeleteListAsync(id);

                    if (!isDeleted)
                    {
                        return NotFound();
                    }

                    return RedirectToAction(nameof(Index));
                }
                
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
