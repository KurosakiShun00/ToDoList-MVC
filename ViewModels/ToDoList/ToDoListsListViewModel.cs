using ToDoList_MVC.Models;

namespace ToDoList_MVC.ViewModels.ToDoList;

public class ToDoListsListViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Remaining => ToDos.Count(x => !x.IsCompleted);
    public List<ToDoDTO> ToDos { get; set; } = new();
}