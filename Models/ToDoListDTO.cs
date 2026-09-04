using ToDoList_MVC.User;
using ToDoList_MVC.ViewModels.ToDoList;

namespace ToDoList_MVC.Models;

public class ToDoListDTO
{
    public ToDoListDTO()
    {
    }

    public ToDoListDTO(ToDoList list)
    {
        Id = list.Id;
        Name = list.Name;
        Description = list.Description;
        UserId = list.UserId;
        ToDos = list.ToDos.Select(t => new ToDoDTO(t)).ToList();
    }

    public ToDoListDTO(ToDoListsCreateViewModel list)
    {
        Name = list.Name;
        Description = list.Description;
    }

    public ToDoListDTO(ToDoListsEditViewModel list)
    {
        Id = list.Id;
        Name = list.Name;
        Description = list.Description;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }


    public string UserId { get; set; } = string.Empty;

    public virtual AppUser? User { get; set; }

    public List<ToDoDTO> ToDos { get; set; } = new();
}