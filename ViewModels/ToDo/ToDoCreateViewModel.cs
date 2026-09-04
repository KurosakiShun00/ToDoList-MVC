using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList_MVC.ViewModels.ToDo;

public class ToDoCreateViewModel
{
    public string? Name { get; set; }
    public int ToDoListId { get; set; }
    public string? ListName { get; set; }
    public int? CategoryId { get; set; }
    public DateTime? Deadline { get; set; }

    public List<SelectListItem> Categories { get; set; } = new();
}