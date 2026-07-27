namespace ToDoList_MVC.ViewModels.ToDo;

public class ToDoEditViewModel
{
    public int Id { get; set; }
    
    public string? Name { get; set; }

    public bool IsCompleted { get; set; }
    
    public int ToDoListId { get; set; }
    public string? ListName  { get; set; }

}