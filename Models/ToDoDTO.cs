using ToDoList_MVC.ViewModels.ToDo;

namespace ToDoList_MVC.Models
{
    public class ToDoDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool IsCompleted { get; set; }

        public int ToDoListId { get; set; }
        public int? CategoryId { get; set; }

        
        public ToDoDTO() { }
        public ToDoDTO(ToDo to_do) =>
            (Id, Name, IsCompleted, ToDoListId, CategoryId) = (to_do.Id, to_do.Name, to_do.IsCompleted, to_do.ToDoListId, to_do.CategoryId);

        public ToDoDTO(ToDoCreateViewModel viewModel)
        {
            Name = viewModel.Name;
            IsCompleted = false;
            ToDoListId = viewModel.ToDoListId;
            CategoryId = viewModel.CategoryId;
        }     
        public ToDoDTO(ToDoEditViewModel viewModel)
        {
            Id = viewModel.Id;
            Name = viewModel.Name;
            IsCompleted = viewModel.IsCompleted;
            ToDoListId = viewModel.ToDoListId;
            CategoryId = viewModel.CategoryId;

        }
    }
}
