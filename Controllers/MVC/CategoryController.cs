using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList_MVC.Models;
using ToDoList_MVC.Services;
using ToDoList_MVC.ViewModels.Category;

namespace ToDoList_MVC.Controllers.MVC;

public class CategoryController : Controller
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
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

        var items = (await _service.GetAllCategories(userId)).ToList();

        
        
        var result = new List<CategoryDetailsViewModel>();

        
        foreach (var item in items)
        {
            result.Add(new CategoryDetailsViewModel
                {
                  Id = item.Id,
                  Name = item.Name,
                  Color = item.Color,
                  ToDoCompleted = await _service.ToDoCompletedCount(item.Id, userId),
                  ToDoNotCompleted = await _service.ToDoNotCompletedCount(item.Id, userId)
                }
            );
        }
        
        return View(result);
    }

    [Authorize]
    public IActionResult Create() => View();
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateViewModel viewModel)
    {   
        var userId = GetUserID();
        if(userId == null) return Unauthorized();
            
        var new_category = new Category(viewModel);

        new_category.UserId = userId;
        
        await _service.CreateCategoryAsync(new_category);
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {  
        var userId = GetUserID();
        if(userId  == null) return Unauthorized();
        var item = await _service.GetCategoryById(id);
        if (item == null) return NotFound();
        var viewModel = new CategoryEditViewModel
        {
            Id = item.Id,
            Name = item.Name,
            Color = item.Color
        };
        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryEditViewModel viewModel)
    {
        var userId = GetUserID();
        if(userId  == null) return Unauthorized();
        
        var newCategory = new Category(viewModel);
        
        await _service.UpdateCategoryAsync(id, newCategory);
        return RedirectToAction(nameof(Index));
        
    }
}