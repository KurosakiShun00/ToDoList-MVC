using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.Models;
using ToDoList_MVC.Services;
using ToDoList_MVC.ViewModels;

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
            return RedirectToAction(nameof(Index));
        }

/*
                // GET: /Customers/Delete/5
                public async Task<IActionResult> Delete(string id)
                {

                    var item = await _service.GetByIDAsync(id);
                    if (item == null) return NotFound();
                    var viewModel = new CustomersDetailViewModel
                    {
                        Id = item.Id,
                        CompanyName = item.CompanyName,
                        ContactName = item.ContactName,
                        ContactTitle = item.ContactTitle,
                        Address = item.Address,
                        City = item.City,
                        Region = item.Region,
                        PostalCode = item.PostalCode,
                        Country = item.Country,
                        Phone = item.Phone,
                        Fax = item.Fax
                    };
                    return View(viewModel);

                }

                // POST: /Customers/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(string id)
                {

                    bool isDeleted = await _service.DeleteAsync(id);

                    if (!isDeleted)
                    {
                        return NotFound();
                    }

                    return RedirectToAction(nameof(Index));
                }
    }

    */
}