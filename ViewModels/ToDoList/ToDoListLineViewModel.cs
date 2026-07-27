namespace ToDoList_MVC.ViewModels;

public class ToDoListLineViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsCompleted { get; set; }
}