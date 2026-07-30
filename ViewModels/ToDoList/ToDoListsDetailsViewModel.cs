namespace ToDoList_MVC.ViewModels.ToDoList;

public class ToDoListsDetailsViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Remaining => ToDos.Count(x => !x.IsCompleted);
        
    public List<ToDoListLineViewModel> ToDos { get; set; } = new();
}