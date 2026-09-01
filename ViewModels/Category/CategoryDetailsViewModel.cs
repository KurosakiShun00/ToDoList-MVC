namespace ToDoList_MVC.ViewModels.Category;

public class CategoryDetailsViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Color { get; set; } = "#C8C6C4";
    public int ToDoCompleted { get; set; }
    public int ToDoNotCompleted { get; set; }
    
    public int ToDoTotal => ToDoCompleted + ToDoNotCompleted;
}