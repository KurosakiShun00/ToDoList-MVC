using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.ViewModels.Shared;
using System.Diagnostics;

namespace ToDoList_MVC.Controllers.MVC
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
    }
}