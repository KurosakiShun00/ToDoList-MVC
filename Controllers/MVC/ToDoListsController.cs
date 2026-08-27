using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList_MVC.Models;
using ToDoList_MVC.Services; 
using ToDoList_MVC.ViewModels.ToDo;
using ToDoList_MVC.ViewModels.ToDoList;
using ToDoList_MVC.ViewModels.Shared;

namespace ToDoList_MVC.Controllers.MVC;

public class ToDoListsController : Controller
{
    private readonly IToDoListService _service;
    private readonly ICategoryService _categoryService;

        public ToDoListsController(IToDoListService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        private string? GetUserID()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        
        [Authorize]
        public async Task<IActionResult> Index(string? filter = null)
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

            ViewData["Filter"] = filter;
            
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
                    IsCompleted = ToDo.IsCompleted,
                    CategoryName = ToDo.Category?.Name,
                    LineColor = ToDo.Category?.Color,
                    CategoryId = ToDo.CategoryId
                });
            }

            var categories = await _categoryService.GetAllCategories(userId);
            
            var viewModel = new ToDoListsDetailsViewModel()
            {
               Id               = item.Id,
               Name           = item.Name,
               Description = item.Description,
               ToDos = ToDos,
               Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList()
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
            var listNumber = (await _service.GetAllListsAsync(userId)).Count();
            if (!ModelState.IsValid) return View(viewModel);
            var errorModel = new ErrorViewModel();
            if (listNumber >= 30)
            {
                ViewData["ErrorTitle"] = "Raggiunto limite di 30 liste";
                ViewData["ErrorMessage"] = "Non è possibile creare un'altra lista in quanto si è raggiunto il limite massimo di 30 liste.";
                        
                errorModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                        
                return View("~/Views/Shared/Error.cshtml", errorModel);
            }
            
            
            var new_list = new ToDoListDTO(viewModel);

            await _service.CreateListAsync(new_list, userId);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DuplicateList(int id)
        {
            var userId = GetUserID();
            if(userId  == null) return Unauthorized();
            var listNumber = (await _service.GetAllListsAsync(userId)).Count();
            var errorModel = new ErrorViewModel();

            if (listNumber >= 30)
            {
                ViewData["ErrorTitle"] = "Raggiunto limite di 30 liste";
                ViewData["ErrorMessage"] = "Non è possibile creare un'altra lista in quanto si è raggiunto il limite massimo di 30 liste.";
                        
                errorModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                        
                return View("~/Views/Shared/Error.cshtml", errorModel);
            }
            var list = await _service.GetByIdAsync(id, userId);
            
            if(list == null) return NotFound();
            if(string.Concat(list.Name, "-Copia").Length<=20)list.Name = string.Concat(list.Name, "-Copia");
            
            var newList = await _service.CreateListAsync(list, userId);

            foreach (var toDo in list.ToDos)
            {
                await _service.AddToDoToListAsync(newList.Id, toDo, userId);
            }
            
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

            var categories = await _categoryService.GetAllCategories(userId);
            
            var viewModel = new ToDoCreateViewModel()
            {
                ToDoListId = id,
                ListName = listName,
                Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList()
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
                Name = item.Name,
                Description = item.Description
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

            var new_list = new ToDoListDTO(viewModel);
            
            await _service.UpdateListAsync(id, new_list, userId);
            return RedirectToAction(nameof(Details), new {id});
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
                public async Task<IActionResult> MultipleDelete(int[] SelectedListIds)
                {
                    var userId = GetUserID();
                    if (userId == null) return Unauthorized();

                    if ( SelectedListIds.Length == 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var id in SelectedListIds)
                    {
                        bool isDeleted = await _service.DeleteListAsync(id, userId);
                        if (!isDeleted)
                        {
                            return NotFound();
                        }
                    }
    
                    return RedirectToAction(nameof(Index));
                }
                
                
                [Authorize]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> MultipleToDoDelete(int[] selectedIds, int toDoListId)
                {
                    var userId = GetUserID();
                    if (userId == null) return Unauthorized();

                    if (selectedIds.Length > 0)
                    {
                        foreach (var id in selectedIds)
                        {
                            var isDeleted = await _service.DeleteToDoAsync(id, userId);
                            if (!isDeleted) return NotFound();
                        }
                    }

                    return RedirectToAction(nameof(Details), new { id = toDoListId });
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
                public async Task<IActionResult> MultipleToDoFinished(int[] selectedIds, int toDoListId)
                {
                    var userId = GetUserID();
                    if (userId == null) return Unauthorized();

                    if (selectedIds.Length > 0)
                    {
                        foreach (var id in selectedIds)
                        {
                            var item = await _service.GetToDoAsync(id);
                            if (item != null)
                            {
                                item.IsCompleted = true;
                                await _service.UpdateToDoAsync(id, item);
                            }
                        }
                    }

                    return RedirectToAction(nameof(Details), new { id = toDoListId });
                }

                [Authorize]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> MultipleFinished(int[] selectedListIds)
                {
                    var userId = GetUserID();
                    if (userId == null) return Unauthorized();
                    if (selectedListIds.Length <= 0) return RedirectToAction(nameof(Index));
                    foreach (var id in selectedListIds)
                    {
                        var toDos = await _service.GetToDosFromListAsync(id, userId);
                        if (toDos == null) continue;
                        foreach (var item in toDos)
                        {
                            item.IsCompleted = true;
                            await _service.UpdateToDoAsync(item.Id, item);
                        }

                    }

                    return RedirectToAction(nameof(Index));
                }
                
                [Authorize]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> MultipleNotFinished(int[] selectedListIds)
                {
                    var userId = GetUserID();
                    if (userId == null) return Unauthorized();
                    if (selectedListIds.Length <= 0) return RedirectToAction(nameof(Index));
                    foreach (var id in selectedListIds)
                    {
                        var toDos = await _service.GetToDosFromListAsync(id, userId);
                        if (toDos == null) continue;
                        foreach (var item in toDos)
                        {
                            item.IsCompleted = false;
                            await _service.UpdateToDoAsync(item.Id, item);
                        }

                    }

                    return RedirectToAction(nameof(Index));
                }
                [Authorize]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> MultipleToDoNotFinished(int[] selectedIds, int toDoListId)
                {
                    var userId = GetUserID();
                    if (userId == null) return Unauthorized();

                    if (selectedIds.Length > 0)
                    {
                        foreach (var id in selectedIds)
                        {
                            var item = await _service.GetToDoAsync(id);
                            if (item != null)
                            {
                                item.IsCompleted = false;
                                await _service.UpdateToDoAsync(id, item);
                            }
                        }
                    }

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
                    existingToDo.CategoryId = viewModel.CategoryId;

                    await _service.UpdateToDoAsync(id, existingToDo);

                    int redirectId = viewModel.ToDoListId != 0 ? viewModel.ToDoListId : existingToDo.ToDoListId;
                    return RedirectToAction(nameof(Details), new { id = redirectId });
                }
                
                [Authorize]
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteToDo(int id)
                {
                    var  userId = GetUserID();
                    if(userId  == null) return Unauthorized();
                    
                    bool isDeleted = await _service.DeleteToDoAsync(id, userId);

                    if (!isDeleted)
                    {
                        return NotFound();
                    }

                    string referer = Request.Headers["Referer"].ToString();

                    if (!string.IsNullOrEmpty(referer))
                    {
                        return Redirect(referer);
                    }

                    
                    return RedirectToAction(nameof(Index));
                }

                public async Task<IActionResult> ExportList(int id)
                {
                    var  userId = GetUserID();
                    if(userId  == null) return Unauthorized();
                    
                    var list = await _service.GetByIdAsync(id, userId);
                    
                    if(list == null) return NotFound();

                    var stringBuilder = new StringBuilder();

                    stringBuilder.Append("====================================================").AppendLine();
                    stringBuilder.Append("LISTA DI ATTIVITA': ").Append(list.Name).AppendLine();
                    stringBuilder.Append("DELL'UTENTE: ").Append(User.FindFirstValue(ClaimTypes.GivenName  )?? "nome non trovato".ToUpper()).AppendLine();
                    stringBuilder.Append("E-MAIL: ").Append(User.FindFirstValue(ClaimTypes.Email  )?? "e-mail non trovata").AppendLine();
                    stringBuilder.Append("scaricata in data: ").Append(DateTime.Now).AppendLine();
                    stringBuilder.Append("====================================================").AppendLine();

                    if (list.ToDos.Count == 0) stringBuilder.Append("LA LISTA E' VUOTA");
                    else
                    {
                        foreach (var toDo in list.ToDos)
                        {
                            stringBuilder.Append(toDo.Name + ", ").Append(toDo.IsCompleted? "Completata" : "Non Completata").AppendLine();
                        }
                    }
                    
                    var str = stringBuilder.ToString();
                    
                    byte[] fileBytes = Encoding.UTF8.GetBytes(str);

                    string fileName = $"{list.Name??"UnnamedList".Replace(" ", "_")}_Export.txt";
                    
                    return File(fileBytes, "text/plain", fileName);
                }
                

                [Authorize]
                [HttpPost]
                public async Task<IActionResult> ImportList(IFormFile file)
                {
                    string? userId = GetUserID();
                    var listNumber = (await _service.GetAllListsAsync(userId)).Count();
                    var errorModel = new ErrorViewModel();
                    
                    if (listNumber >= 30)
                    {
                        ViewData["ErrorTitle"] = "Raggiunto limite di 30 liste";
                        ViewData["ErrorMessage"] = "Non è possibile creare un'altra lista in quanto si è raggiunto il limite massimo di 30 liste.";
                        
                        errorModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                        
                        return View("~/Views/Shared/Error.cshtml", errorModel);
                    }
                    
                    if (file.Length == 0)
                    {
                        ViewData["ErrorTitle"] = "Seleziona un file .txt valido.";
                        ViewData["ErrorMessage"] = "Il file non risulta avere del contenuto";
                        
                        errorModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                        
                        return View("~/Views/Shared/Error.cshtml", errorModel);
                    }
                    

                    string nomeLista = "Lista Importata";
                    var toDos = new List<ToDoDTO>();
                    
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        string? linea;

                        while ((linea = await reader.ReadLineAsync()) != null)
                        {
                            linea = linea.Trim();
                            
                            if (linea.StartsWith("LISTA DI ATTIVITÀ:", StringComparison.OrdinalIgnoreCase))
                            {
                                var partiHeader = linea.Split(':');
                                if (partiHeader.Length > 1 && !string.IsNullOrWhiteSpace(partiHeader[1]))
                                {
                                    nomeLista = partiHeader[1].Trim();
                                }
                                continue;
                            }
            
                            if (linea.StartsWith('=') || 
                                linea.StartsWith("DELL'UTENTE:") || 
                                linea.StartsWith("E-MAIL:") || 
                                linea.StartsWith("scaricata in data:") || 
                                string.IsNullOrWhiteSpace(linea))
                            {
                                continue;
                            }
                            
                            var parti = linea.Split(',');
                            
                            if (parti.Length != 2) continue;
                            
                            string nomeToDo = parti[0].Trim();
                            string stato = parti[1].Trim();
            
                            var isCompleted = stato.Equals("Completata", StringComparison.OrdinalIgnoreCase);
            
                            toDos.Add(new ToDoDTO()
                            {
                                Name = nomeToDo,
                                IsCompleted = isCompleted
                            });
                        }
                    }
            
                    if (!toDos.Any())
                    {
                        ViewData["ErrorTitle"] = "Seleziona un file .txt valido.";
                        ViewData["ErrorMessage"] = "Il file non risulta avere attività da importare";
                        
                        errorModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                        
                        return View("~/Views/Shared/Error.cshtml", errorModel);
                    }
            
                    
                    var nuovaLista = new ToDoListDTO()
                    {
                        Name = nomeLista,
                        UserId = userId!
                    };
            
                    var newList = await _service.CreateListAsync(nuovaLista, userId);

                    foreach (var toDo in toDos)
                    {
                        await _service.AddToDoToListAsync(newList.Id, toDo, userId);
                    }
                    
                    return RedirectToAction(nameof(Index));
                }
}
