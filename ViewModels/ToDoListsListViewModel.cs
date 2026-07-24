using ToDoList_MVC.Models;
namespace ToDoList_MVC.ViewModels;

public class ToDoListsListViewModel
{
    public string? Name { get; set; }

    public List<ToDo> ToDos { get; set; } = new();
}