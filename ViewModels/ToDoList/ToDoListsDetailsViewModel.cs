using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList_MVC.ViewModels.ToDoList;

public class ToDoListsDetailsViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Remaining => ToDos.Count(x => !x.IsCompleted);
    public string? Description { get; set; }
    
    public int? CategoryId { get; set; }
    
    public List<ToDoListLineViewModel> ToDos { get; set; } = new();
    public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
}