using ToDoList_MVC.ViewModels.ToDoList;
using Microsoft.AspNetCore.Identity;

namespace ToDoList_MVC.Models
{
    public class ToDoListDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public string UserId { get; set; } = string.Empty;

        public virtual IdentityUser? User { get; set; }
        
        public List<ToDoDTO> ToDos { get; set; } = new();

        public ToDoListDTO() { }

        public ToDoListDTO(ToDoList list)
        {
            Id = list.Id;
            Name = list.Name;
            UserId = list.UserId; 
            ToDos = list.ToDos.Select(t => new ToDoDTO(t)).ToList();
        }
        
        public ToDoListDTO(ToDoListsCreateViewModel list)
        {
            Name = list.Name;
        }  

        public ToDoListDTO(ToDoListsEditViewModel list)
        {
            Id = list.Id;
            Name = list.Name;
        }
    }
}