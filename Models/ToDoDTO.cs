namespace ToDoList_MVC.Models
{
    public class ToDoDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool IsCompleted { get; set; }

        public ToDoDTO(ToDo to_do) =>
            (Id, Name, IsCompleted) = (to_do.Id, to_do.Name, to_do.IsCompleted);
    }
}
