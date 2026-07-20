using Microsoft.AspNetCore.Mvc;

namespace ToDoList_MVC.Controllers.MVC
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}