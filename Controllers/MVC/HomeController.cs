using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.ViewModels.Shared;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ToDoList_MVC.Services;
using ToDoList_MVC.ViewModels.ToDoList;

namespace ToDoList_MVC.Controllers.MVC
{
    public class HomeController : Controller
    {
        private readonly IToDoListService _service;

        public HomeController(IToDoListService service)
        {
            _service = service;
        }
        
        private string? GetUserID()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        
        [HttpGet]
        public IActionResult Calendar()
        {
            return View();
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
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/Error/{statusCode?}")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == 404)
            {
                ViewData["ErrorTitle"] = "404 - Pagina Non Trovata";
                ViewData["ErrorMessage"] = "La pagina che stai cercando non esiste";
            }
            else
            {
                ViewData["ErrorTitle"] = "Qualcosa è andato storto...";
                ViewData["ErrorMessage"] = "Si è verificato un errore imprevisto nel server durante l'elaborazione della richiesta.";
            }

            var viewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
        
        [Authorize]
        public IActionResult Import()
        {
            return View();
        }
    }
}