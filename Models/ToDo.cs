
namespace ToDoList_MVC.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool IsCompleted { get; set; }

        //fk

        public int ToDoListId { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
