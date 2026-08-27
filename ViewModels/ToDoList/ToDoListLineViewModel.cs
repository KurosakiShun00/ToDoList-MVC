namespace ToDoList_MVC.ViewModels.ToDoList;

public class ToDoListLineViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsCompleted { get; set; }
    public string? CategoryName { get; set; }
    public int? CategoryId { get; set; }
    public string? LineColor { get; set; }
}