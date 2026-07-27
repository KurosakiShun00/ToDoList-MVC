using ToDoList_MVC.ViewModels;

namespace ToDoList_MVC.Models
{
    public class ToDoListDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ToDoDTO> ToDos { get; set; } = new();

        public ToDoListDTO() { }

        public ToDoListDTO(ToDoList list)
        {
            Id = list.Id;
            Name = list.Name;
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
