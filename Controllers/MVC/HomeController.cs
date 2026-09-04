using System.Diagnostics;
using System.Security.Claims;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.Services;
using ToDoList_MVC.User;
using ToDoList_MVC.ViewModels.Shared;
using ToDoList_MVC.ViewModels.ToDoList;

namespace ToDoList_MVC.Controllers.MVC;

public class HomeController : Controller
{
    private readonly IToDoListService _service;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(IToDoListService service, UserManager<AppUser> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    private string? GetUserID()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Calendar()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var ApplicationUser = await _userManager.GetUserAsync(User);
        if (ApplicationUser == null)
        {
            var errorModel = new ErrorViewModel();
            ViewData["ErrorTitle"] = "Errore di autenticazione";
            ViewData["ErrorMessage"] = "Si è verificato un errore di autenticazione, riprovare il login.";

            errorModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View("~/Views/Shared/Error.cshtml", errorModel);
        }

        var mail = ApplicationUser.Email ?? "";
        var atIndex = mail.LastIndexOf('@');

        var username = atIndex > 0
            ? mail.Truncate(atIndex, "")
            : !string.IsNullOrEmpty(mail)
                ? mail
                : "Ospite";

        ViewData["nickname"] = ApplicationUser.NickName ?? username;
        ViewData["sesso"] = ApplicationUser.Sesso;

        var userId = GetUserID();
        if (userId == null) return Unauthorized();

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
            ViewData["ErrorMessage"] =
                "Si è verificato un errore imprevisto nel server durante l'elaborazione della richiesta.";
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