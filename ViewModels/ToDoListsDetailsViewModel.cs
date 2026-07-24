using ToDoList_MVC.Models;
namespace ToDoList_MVC.ViewModels;

public class ToDoListsDetailsViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<ToDo> ToDos { get; set; } = new();
}