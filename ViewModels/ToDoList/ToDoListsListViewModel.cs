using ToDoList_MVC.Models;
namespace ToDoList_MVC.ViewModels;

public class ToDoListsListViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<ToDoDTO> ToDos { get; set; } = new();
}