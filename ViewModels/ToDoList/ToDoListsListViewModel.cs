using ToDoList_MVC.Models;

namespace ToDoList_MVC.ViewModels.ToDoList;

public class ToDoListsListViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Remaining => ToDos.Count(x => !x.IsCompleted);
    public bool IsFinished => Remaining == 0 && ToDos.Count != 0;
    public int ExpiredCount => ToDos.Count(x => x.Deadline<DateTime.Now);
    public List<ToDoDTO> ToDos { get; set; } = new();
}